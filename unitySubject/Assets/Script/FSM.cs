using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//State的轉換
public enum eTransitionID {
	None = -1,
	Idle_To_Track,
	Idle_To_Chase,
	Idle_To_Attack,
	Track_To_Idle,
	Track_To_Chase,
	Track_To_Attack,
	Track_To_Skill,
	Track_To_Wander,
	Wander_To_Idle,
	Chase_To_Attack,
	Chase_To_Idle,
	Attack_To_Track,
	Attack_To_Chase,
	Attack_To_Idle,
	Skill_To_Track,
	Skill_To_Chase,
	Skill_To_Idle,
	Idle_To_Wander
}

//目前State
public enum eStateID {
	None = -1,
	Idle = 0, //閒置
	Wander, //閒逛
	Track, //追蹤
	Chase, //追逐
	Attack,
	Skill,
	escape
}

//目前State
public enum ePreviousStateID {
	None = -1,
	Idle = 0, //閒置
	Wander, //閒逛
	Track, //追蹤
	Chase, //追逐
	Attack,
	escape
}

//abstract代表要被繼承
//紀錄某State該連到哪一個State
public abstract class FSMBaseState {
	public Dictionary<eTransitionID, eStateID> m_StateMap = new Dictionary<eTransitionID, eStateID> ();//給一個Key，然後把對應的值找出來
	public eStateID m_StateID;
	public ePreviousStateID m_PreviousStateID;
	
	//增加Transition(對應的TransitionID,要去的StateID)
	public void AddTransition(eTransitionID tID, eStateID sID){
		//不處理
		if(tID == eTransitionID.None || sID == eStateID.None){
			return;
		}
		//m_StateMap是否包含TransitionID，是就不處理
		if(m_StateMap.ContainsKey(tID)){
			return;
		}
		m_StateMap.Add(tID, sID);
	}
	
	//刪除Transition
	public void RemoveTranstion(eTransitionID tID){
		//不處理
		if (tID == eTransitionID.None) {
			return;
		}
		//m_StateMap有包含TransitionID，就刪除
		if (m_StateMap.ContainsKey (tID)) {
			m_StateMap.Remove (tID);
		}
	}
	
	//給一個TransitionID，把要output出去的StateID寫出來
	public eStateID GetOutputStateID(eTransitionID tID){
		if (m_StateMap.ContainsKey (tID)) {
			return m_StateMap [tID];
		}
		return eStateID.None;
	}
	
	//現在這個Transition有沒有被達成是否該切換
	public abstract void CheckState (AIData data);
	//做State
	public abstract void DoState (AIData data);
	
	//virtual 關鍵字的用途是修改方法、屬性、索引子 (Indexer) 或事件宣告，以及允許在衍生類別 (Derived Class) 中予以覆寫。例如，這個方法可由任一繼承它的類別來覆寫
	//當離開State前要做什麼
	public virtual void DoBeforeEnter (AIData data){
		
	}
	//當進去State前要做什麼
	public virtual void DoBeforeLeave (AIData data){
		
	}
}

//要有一個管理員去管理這些State
public class FSMManager{
	
	//所有繼承EnemyState的東西都可以轉型成EnemyState存起來
	private List<FSMBaseState> m_State;
	
	//紀錄目前的State
	private eStateID m_currentStateID;
	public eStateID CurrentStateID() { return m_currentStateID; }
	
	//紀錄目前的State是什麼
	private FSMBaseState m_currentState;
	public FSMBaseState CurrentState() { return m_currentState; }
	
	public FSMManager () {
		m_State = new List<FSMBaseState>();
	}

	public void DoState(AIData data){
		m_currentState.CheckState (data);
		m_currentState.DoState (data);
	}

	public void AddState(FSMBaseState state){
		if (state == null) {
			return;
		}
		//如果是空的就直接加
		int iCount = m_State.Count;
		if (iCount == 0) {
			m_State.Add (state);
			m_currentState = state;
			m_currentStateID = state.m_StateID;
			return;
		}
		//檢查有無重複
		for (int i = 0; i < iCount; i++) {
			if(m_State[i].m_StateID == state.m_StateID){
				return;
			}
		}
		m_State.Add (state);
	}
	
	public void DeleteState(eStateID sID){
		if (sID == eStateID.None) {
			return;
		}
		int iCount = m_State.Count;
		for (int i = 0; i < iCount; i++) {
			if(m_State[i].m_StateID == sID){
				m_State.RemoveAt(i);
				return;
			}
		}
	}
	
	//做State的切換
	public void PerformTransition(eTransitionID tID, AIData data){
		if(tID == eTransitionID.None){
			return;
		}
		eStateID sID = m_currentState.GetOutputStateID (tID);
		if (sID == eStateID.None) {
			return;
		}
		m_currentStateID = sID;
		int iCount = m_State.Count;
		for (int i = 0; i < iCount; i++) {
			if (m_State [i].m_StateID == m_currentStateID) {
				m_currentState.DoBeforeLeave (data);
				m_currentState = m_State [i];
				m_currentState.DoBeforeEnter (data);
				break;
			}
		}
	}
}
//////////////////////////////////////////////////////////////////////////
/*
敵人FSM
*/
//////////////////////////////////////////////////////////////////////////
public class FSMNpcIdleState : FSMBaseState{
	
	private float fTime;
	private float fIdleTime;

	public FSMNpcIdleState(){
		m_StateID = eStateID.Idle;
	}

	public override void DoBeforeEnter (AIData data){
		data.targetPoint = null;
		fTime = 0.0f;
		fIdleTime = 1.0f;
	}
	
	//因為是複寫的，所以是override
	public override void CheckState (AIData data){
		if(fTime > fIdleTime){
			//fTime = 0.0f;
			data.m_State.PerformTransition(eTransitionID.Idle_To_Track, data);
		}
	}
	
	public override void DoState (AIData data){
		Debug.Log ("fTime："+fTime);
		Debug.Log ("fIdleTime："+fIdleTime);
		Debug.Log (data.thisPoint+"目前狀態：" + m_StateID);
		fTime += Time.deltaTime;
	}
}

public class FSMNpcTrackState : FSMBaseState{
	
	private float fTime;
	private float fTrackTime;
	GameObject m_GameObject = null;
	NPC nComponent = null;
	float fDist;
	int iSlotFSM;
	int iRandom;
	
	public FSMNpcTrackState(){
		m_StateID = eStateID.Track;
	}

	public override void DoBeforeEnter (AIData data)
	{
		data.targetPoint = null;
		fTime = 0.0f;
		fTrackTime = 2.0f;
	}
	
	//因為是複寫的，所以是override
	public override void CheckState (AIData data){
		//尋找目標
		fDist = 10000.0f;
		iSlotFSM = -1;
		data.targetPoint = AIBehavior.CheckPlayer (data, out fDist, out iSlotFSM);
		if (data.targetPoint != null) {
			Debug.Log ("有目標");
			data.targetPosition = data.targetPoint.transform.position;
			//if (fDist < data.fDetectLength) { //距離小於可視範圍
			//	data.m_State.PerformTransition (eTransitionID.Track_To_Chase, data);
			//} else
			if (fDist < data.fAttackLength) { //距離小於攻擊範圍
				if (data.fMP > data.fSkillMP) {
					//=============================技能進入機率=============================
					iRandom = Random.Range (1, 2);
					if (iRandom == 2) {
					//=============================完=============================
						data.m_State.PerformTransition (eTransitionID.Track_To_Attack, data);
					} else {
						data.m_State.PerformTransition (eTransitionID.Track_To_Skill, data);
					}
				} else{
					data.m_State.PerformTransition (eTransitionID.Track_To_Attack, data);
				}
			}
		}
	}
	
	public override void DoState (AIData data){
		Debug.Log ("前往目標");
		m_GameObject = data.thisPoint;
		nComponent = m_GameObject.GetComponent<NPC> ();
		//如果沒有小於攻擊範圍，就追逐
		if (data.iAstarIndex > -1) {
			Debug.Log ("iAstarIndex" + data.iAstarIndex);
			Debug.Log ("GetPathPointNumber" + (nComponent.m_AStar.GetPathPointNumber () - 1));
			if (data.iAstarIndex < (nComponent.m_AStar.GetPathPointNumber () - 1)) {
				//多久做一次找seek點，會影響效能
				//Debug.Log ("多久做一次找seek點，會影響效能");
				nComponent.m_AStar.GetAStarSeekPoint (data, ref data.iAstarIndex, ref data.targetPosition);
				//m_AStar.GetAStarSeekPoint (this.transform.position, ref m_AIData.iAstarIndex, ref m_AIData.targetPosition);
			}
			//m_Seek (this.gameObject, m_AIdata);
			if (AIBehavior.OBS (data.thisPoint, data, false) == false) { //如果沒有撞到東西
				if (AIBehavior.Seek (data.thisPoint, data) == false) {
					data.iAstarIndex = -1;
				}
			}
			AIBehavior.MoveForward (data.thisPoint, data);
			//一定時間重新計算目標
			
			if (fTime > fTrackTime) {
				data.targetPoint = null; //把目標清掉
				data.iAstarIndex = -1;
				fTime = 0.0f;
			}
			fTime += Time.deltaTime;
			//返回
			return;
		} else {
			if (nComponent.m_AStar.AStarSearch (nComponent.m_AStar.transform.position, data.targetPosition)) { //如果傳給Astar的結果為true，iAstarIndex設為0
				data.iAstarIndex = 0;
			}
		}
		Debug.Log (data.thisPoint+"目前狀態：" + m_StateID);
	}

	public override void DoBeforeLeave (AIData data){
		data.iAstarIndex = -1;
	}	
}

public class FSMNpcAttackState : FSMBaseState{

	public float fTime;
	public float fAttackTime; //攻擊動作的時間
	GameObject m_pGameObject = null;
	Player pComponent = null;
	float fDist;
	int iSlotFSM;
	//=============================測試用=============================
	float fScale = 0.0f;
	float fScaleMax = 2.0f;
	//=============================完=============================
	
	public FSMNpcAttackState(){
		m_StateID = eStateID.Attack;
	}
	
	public override void DoBeforeEnter (AIData data)
	{
		Vector3 tVec = data.targetPoint.transform.position - data.thisPoint.transform.position;
		tVec.y = 0.0f;
		tVec.Normalize();
		data.thisPoint.transform.forward = tVec;
		fTime = 0.0f;
		fAttackTime = 1.0f;
	}
	
	//因為是複寫的，所以是override
	public override void CheckState (AIData data){
		fDist = 10000.0f;
		iSlotFSM = -1;
		data.targetPoint = AIBehavior.CheckPlayer (data, out fDist, out iSlotFSM);
		if (data.targetPoint != null) {
			//m_AIData.targetAIData = m_AIData.targetPoint;
			if (fDist >= data.fAttackLength) { //如果距離大於攻擊範圍，切到追逐狀態
				data.m_State.PerformTransition (eTransitionID.Attack_To_Track, data);
			}
		} else{ //回到Idel狀態
			data.m_State.PerformTransition (eTransitionID.Attack_To_Idle, data);
		}
	}
	
	public override void DoState (AIData data){
		Debug.Log (data.thisPoint + "目前狀態：" + m_StateID);
		//取出Component
		m_pGameObject = data.targetPoint;
		pComponent = m_pGameObject.GetComponent<Player> ();
		//m_nGameObject = data.thisPoint;
		//nComponent = m_nGameObject.GetComponent<NPC> ();
		//完
		//=============================測試用=============================
		Color r = Color.black;
		r.r = Random.Range (0.0f, 1.0f);
		r.g = Random.Range (0.0f, 1.0f);
		r.b = Random.Range (0.0f, 1.0f);
		data.thisPoint.GetComponent<Renderer> ().material.color = r;
		//=============================完=============================
		if (fTime > fAttackTime) {
			pComponent.m_AIData.fHP -= data.fAttack;
			data.fMP += 5.0f;
			//============================攻擊紀錄============================
			Debug.Log (data.thisPoint + "===普通攻擊擊中===" + m_pGameObject);
			Debug.Log (m_pGameObject + "===被普通攻擊===" + data.fAttack + "===擊中後的血量===" + pComponent.m_AIData.fHP);
			//============================完============================
			//=============================防呆=============================
			if (pComponent.m_AIData.fHP <= 0) { pComponent.m_AIData.fHP = 0; }
			if (data.fMP >= data.fMaxMP) { data.fMP = data.fMaxMP; }
			//=============================完=============================
			Debug.Log (pComponent.m_AIData.fHP);
			if (pComponent.m_AIData.fHP == 0.0f) {
				ObjectPool.m_Instance.UnLoadObjectToPool (out iSlotFSM, data.targetPoint.gameObject);
				data.m_State.PerformTransition (eTransitionID.Attack_To_Idle, data);
			}
			//=============================測試用=============================
			data.thisPoint.GetComponent<Renderer> ().material.color = Color.white;
			//=============================完=============================
			data.m_State.PerformTransition (eTransitionID.Attack_To_Idle, data);
		}
		fTime += Time.deltaTime;
	}
}

public class FSMNpcSkillState : FSMBaseState{
	
	public float fTime;
	public float fSkillTime; //攻擊動作的時間
	GameObject m_pGameObject = null;
	Player pComponent = null;
	float fDist;
	int iSlotFSM;
	int iRandom;
	//=============================測試用=============================
	float fScale = 0.0f;
	float fScaleMax = 2.0f;
	//=============================完=============================
	
	public FSMNpcSkillState(){
		m_StateID = eStateID.Skill;
	}
	
	public override void DoBeforeEnter (AIData data)
	{
		Vector3 tVec = data.targetPoint.transform.position - data.thisPoint.transform.position;
		tVec.y = 0.0f;
		tVec.Normalize();
		data.thisPoint.transform.forward = tVec;
		fTime = 0.0f;
		fSkillTime = 1.0f;
	}
	
	//因為是複寫的，所以是override
	public override void CheckState (AIData data){
		fDist = 10000.0f;
		iSlotFSM = -1;
		data.targetPoint = AIBehavior.CheckPlayer (data, out fDist, out iSlotFSM);
		if (data.targetPoint != null) {
			//m_AIData.targetAIData = m_AIData.targetPoint;
			if (fDist >= data.fAttackLength) { //如果距離大於攻擊範圍，切到追逐狀態
				data.m_State.PerformTransition (eTransitionID.Skill_To_Track, data);
			}
		} else{ //回到Idel狀態
			data.m_State.PerformTransition (eTransitionID.Skill_To_Idle, data);
		}
	}
	
	public override void DoState (AIData data){
		Debug.Log (data.thisPoint + "目前狀態：" + m_StateID);
		//取出Component
		m_pGameObject = data.targetPoint;
		pComponent = m_pGameObject.GetComponent<Player> ();
		//完
		//=============================測試用=============================
		if (fScale < fScaleMax) {
			data.thisPoint.transform.localScale += new Vector3 (1.0f, 1.0f, 1.0f) * Time.deltaTime;
		} else {
			data.thisPoint.transform.localScale -= new Vector3 (1.0f, 1.0f, 1.0f) * Time.deltaTime;
			if (data.thisPoint.transform.localScale.x <= 5.0f){ fScale = 0.0f; }
		}
		fScale += Time.deltaTime;
		//=============================完=============================
		if (fTime > fSkillTime) {
			pComponent.m_AIData.fHP -= data.fSkill;
			data.fMP -= data.fSkillMP;
			//============================攻擊紀錄============================
			Debug.Log (data.thisPoint + "===技能攻擊擊中===" + m_pGameObject);
			Debug.Log (m_pGameObject + "===被技能攻擊===" + data.fSkill + "===擊中後的血量===" + pComponent.m_AIData.fHP);
			//=============================完=============================
			//=============================防呆=============================
			if (pComponent.m_AIData.fHP <= 0) { pComponent.m_AIData.fHP = 0; }
			//=============================完=============================
			Debug.Log (pComponent.m_AIData.fHP);
			if (pComponent.m_AIData.fHP == 0.0f) {
				Debug.Log (data.thisPoint + "===攻擊目標===" + data.targetPoint.gameObject + "===死亡===");
				ObjectPool.m_Instance.UnLoadObjectToPool (out iSlotFSM, data.targetPoint.gameObject);
				data.m_State.PerformTransition (eTransitionID.Skill_To_Idle, data);
			}
			//=============================測試用=============================
			data.thisPoint.GetComponent<Renderer> ().material.color = Color.white;
			//=============================完=============================
			data.m_State.PerformTransition (eTransitionID.Skill_To_Idle, data);
		}
		fTime += Time.deltaTime;
	}
}

public class FSMNpcWanderState : FSMBaseState{

	private bool bArrival;

	public FSMNpcWanderState(){
		m_StateID = eStateID.Wander;
		bArrival = false;
	}
	
	public override void DoBeforeEnter (AIData data)
	{
		data.targetPosition.x = Random.Range (-20.0f, 20.0f);
		data.targetPosition.y = 1.0f;
		data.targetPosition.z = Random.Range (-20.0f, 20.0f);
	}
	
	//因為是複寫的，所以是override
	public override void CheckState (AIData data){
		if (bArrival) {
			bArrival = false;
			data.m_State.PerformTransition (eTransitionID.Wander_To_Idle, data);
		}
	}
	
	public override void DoState (AIData data){
		Debug.Log (data.thisPoint+"目前狀態：" + m_StateID);
		if (AIBehavior.OBS (data.thisPoint, data, false) == false) { //如果沒有撞到東西
			if (AIBehavior.Seek (data.thisPoint, data) == false) {
				bArrival = true;
				return;
			}
		}
		AIBehavior.MoveForward (data.thisPoint, data);
	}
}
//////////////////////////////////////////////////////////////////////////
/*
玩家FSM
*/
//////////////////////////////////////////////////////////////////////////
public class FSMIdleState : FSMBaseState{
	
	private float fTime;
	private float fAttackTime;
	int iHit;
	Vector3 hitPos;
	bool bArrival;
	float fDist;
	Vector3 tVec;
	GameObject m_Collision;
	bool bTest;
	Vector3 vForward;
	GameObject m_GameObject = null;
	NPC nComponent = null;
	Player pComponent = null;
	int iSlotFSM;

	public FSMIdleState(){
		m_StateID = eStateID.Idle;
		fTime = 0.0f;
		fAttackTime = 1.0f;
		iHit = -1;
		bArrival = false;
		fDist = 0.0f;
	}
	
	public override void DoBeforeEnter (AIData data){
		data.targetPoint = null;
	}
	
	//因為是複寫的，所以是override
	public override void CheckState (AIData data){
		Debug.Log ("bWinOrLose："+SceneManager.m_Instance.bWinOrLose);
		if (SceneManager.m_Instance.bEnd == false) {
			if (Input.GetMouseButtonDown (0)) {
				bTest = false;
				Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit rHit;
				if (Physics.Raycast (r, out rHit, 100000.0f, 1 << LayerMask.NameToLayer ("Enemy"))) {
					if (rHit.collider.gameObject.layer == 10) {//10是Enemy的Layer
						iHit = 0;
						data.targetPoint = rHit.collider.gameObject;
						data.targetPosition = data.targetPoint.gameObject.transform.position;
						tVec = data.targetPosition - data.thisPoint.gameObject.transform.position;
						data.thisPoint.gameObject.transform.forward = tVec;

					} 
				} else if (Physics.Raycast (r, out rHit, 100000.0f, 1 << LayerMask.NameToLayer ("Terrain"))) {
					iHit = 1;
					data.targetPosition = rHit.point;
					tVec = data.targetPosition - data.thisPoint.gameObject.transform.position;
					data.thisPoint.gameObject.transform.forward = tVec;
					Debug.Log (data.targetPosition);
				}
			}
		}
	}
	
	public override void DoState (AIData data){
		Debug.Log (data.thisPoint + "目前狀態：" + m_StateID);
		m_GameObject = data.thisPoint;
		pComponent = m_GameObject.GetComponent<Player>();
		if (bTest == false && vForward == Vector3.zero) {
			if (iHit == 0) {
				pComponent.iNowEgo = Player.eEgo.Run;
				iSlotFSM = -1;
				Debug.Log ("移動到目標中");
				data.targetPosition = data.targetPoint.gameObject.transform.position;
				tVec = data.targetPosition - data.thisPoint.gameObject.transform.position;
				data.thisPoint.transform.forward = tVec;
				fDist = tVec.magnitude;
				ObjectPool.m_Instance.FindObjectToPool(out iSlotFSM, data.targetPoint.gameObject);
				if (fDist < data.fAttackLength) {
					Debug.Log ("到達攻擊範圍");
					pComponent.iNowEgo = Player.eEgo.Attac;
					m_GameObject = data.targetPoint;
					nComponent = m_GameObject.GetComponent<NPC>();
					//攻擊動作
					Debug.Log ("fTime："+fTime);
					Debug.Log ("fAttackTime："+fAttackTime);
					if (fTime > fAttackTime) {
						fTime = 0.0f;
						nComponent.m_AIData.fHP -= data.fAttack;
						if(nComponent.m_AIData.fHP <= 0){
							nComponent.m_AIData.fHP = 0;
						}
						Debug.Log ("======================NPC："+nComponent.m_AIData.fHP);
						if(nComponent.m_AIData.fHP == 0.0f){
							ObjectPool.m_Instance.UnLoadObjectToPool(out iSlotFSM, data.targetPoint.gameObject);
							//return;
							//回到idle狀態
							//data.m_State.PerformTransition (eTransitionID.Attack_To_Idle, data);
						}
						iHit = -1;
						pComponent.iNowEgo = Player.eEgo.Idle;
						data.targetPoint = null;
						//data.m_State.PerformTransition (eTransitionID.Attack_To_Idle, data);
					}
					fTime += Time.deltaTime;
					return;
				} else {
					if (AIBehavior.OBS (data.thisPoint, data, false) == false) {//如果沒有撞到東西
						if (AIBehavior.Seek (data.thisPoint, data) == false) {
							pComponent.iNowEgo = Player.eEgo.Idle;
							bArrival = true;
							return;
						}
					}
					AIBehavior.MoveForward (data.thisPoint, data);
				}
			} else if (iHit == 1) {
				pComponent.iNowEgo = Player.eEgo.Run;
				//Player.m_Instance.iNowEgo = Player.eEgo.Run;
				Debug.Log ("移動到目的地中");
				Debug.Log (data.thisPoint.transform.position);
				data.thisPosition = data.thisPoint.transform.position;
				if (AIBehavior.OBS (data.thisPoint, data, false) == false) {//如果沒有撞到東西
					if (AIBehavior.Seek (data.thisPoint, data) == false) {
						pComponent.iNowEgo = Player.eEgo.Idle;
						bArrival = true;
						return;
					}
				}
				AIBehavior.MoveForward (data.thisPoint, data);
			}
		}
		//bTest = false;
		return;
	}
}