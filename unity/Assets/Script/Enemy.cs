using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public enum eNpcState{
		None = -1,
		Idel = 0, //閒置
		Wander, //閒逛
		Chase, //追逐
		Attack,
		track //追蹤
	}

	public eNpcState mCurrentState = eNpcState.Idel;
	public float fTime = 0.0f;
	public float fIdelTime = 3.0f;
	public float fAttackTime = 2.0f; //攻擊動作的時間

	AIData m_AIData = new AIData ();
	public GameObject targetPoint;
	
	public float m_fMaxSpeed = 10.0f;
	private AStar m_AStar;
	bool bHit = false;
	Vector3 hitPos;

	//FSM委派
	public delegate void StateDelegate();
	public StateDelegate DoState = null;

	//public delegate void AIDelegate(GameObject go, AIdata data);
	//public AIDelegate m_Seek = null;

	// Use this for initialization
	void Start () {
		m_AStar = this.GetComponent<AStar> ();

		m_AIData.fspeed = 0.1f;
		m_AIData.fMaxspeed = m_fMaxSpeed;
		m_AIData.frotate = 0.0f;
		m_AIData.fMaxrotate = 10.0f;
		m_AIData.fColProbe = 3.0f;
		m_AIData.targetPoint = targetPoint;
		m_AIData.m_Obs = SceneManager.m_Instance.m_Obs;
		m_AIData.fRadius = 1.0f;
		m_AIData.iAstarIndex = -1;
		m_AIData.targetPosition = Vector3.zero;
		m_AIData.fDetectLength = 5.0f;
		m_AIData.fAttackLength = 2.0f;

		//m_Seek = AIBehavior.Seek;

		DoState = DoIdel;

	}
	
	// Update is called once per frame
	void Update () {

		DoState ();
		Debug.Log (mCurrentState);


		/*
		//如果已經Astar過了
		if (m_AIData.iAstarIndex > -1) {
			if (m_AIData.iAstarIndex < (m_AStar.GetPathPointNumber () - 1)) {
				//多久做一次找seek點，會影響效能
				m_AStar.GetAStarSeekPoint (ref m_AIData.iAstarIndex, ref m_AIData.targetPosition);
			}
			//m_Seek (this.gameObject, m_AIdata);
			if (AIBehavior.OBS (this.gameObject, m_AIData, false) == false) { //如果沒有撞到東西
				if (AIBehavior.Seek (this.gameObject, m_AIData) == false) {
					m_AIData.iAstarIndex = -1;
				}
			}
			AIBehavior.MoveForward (this.gameObject, m_AIData);
		} 

		if (bHit) {
			//傳起點物件的值，滑鼠點下的值給AStar計算
			if (m_AStar.AStarSearch (m_AStar.transform.position, hitPos)) { //如果傳給Astar的結果為true，iAstarIndex設為0
				m_AIData.iAstarIndex = 0;
			}
			bHit = false;
		} else {
			if (Input.GetMouseButtonDown (0)) {
				Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit rHit;
				if (Physics.Raycast (r, out rHit, 100000.0f, 1 << LayerMask.NameToLayer ("Terrain"))) {
					bHit = true;
					hitPos = rHit.point;
				}
			}
		}
		 */
	}

	//改變狀態
	void ChangeState(eNpcState NextState){
		if (NextState == eNpcState.Attack) {
			//攻擊時將面向面對目標
			Vector3 tVec = m_AIData.targetPoint.transform.position - this.transform.position;
			tVec.y = 0.0f;
			tVec.Normalize();
			this.transform.forward = tVec;
			DoState = DoAttack;
		} else if (NextState == eNpcState.Chase) {
			DoState = DoChase;
		} else if (NextState == eNpcState.Idel) {
			m_AIData.targetPoint = null; //把目標清掉
			DoState = DoIdel;
		} else if (NextState == eNpcState.Wander) {
			//給一個巡邏的範圍
			m_AIData.targetPosition.x = Random.Range (-20.0f, 20.0f);
			m_AIData.targetPosition.y = 0.0f;
			m_AIData.targetPosition.z = Random.Range (-20.0f, 20.0f);
			DoState = DoWander;
		}
		mCurrentState = NextState;
		fTime = 0.0f; //一換State就把Time歸零
	}

	//是否抵達目標
	bool MoveToTarget(){
		//m_Seek (this.gameObject, m_AIdata);
		if (AIBehavior.OBS (this.gameObject, m_AIData, false) == false) { //如果沒有撞到東西
			if (AIBehavior.Seek (this.gameObject, m_AIData) == false) {
				return true;
			}
		}
		AIBehavior.MoveForward (this.gameObject, m_AIData);
		return false;
	}

	//每個狀態另外用Function處理
	void DoIdel(){
		float fDist = 10000.0f;
		m_AIData.targetPoint = CheckPlayer (out fDist); //尋找目標

		if (m_AIData.targetPoint != null) {
			Debug.Log("有目標");
			m_AIData.targetPosition = m_AIData.targetPoint.transform.position;

			if (m_AIData.iAstarIndex > -1) {
				if (m_AIData.iAstarIndex < (m_AStar.GetPathPointNumber () - 1)) {
					//多久做一次找seek點，會影響效能
					m_AStar.GetAStarSeekPoint (ref m_AIData.iAstarIndex, ref m_AIData.targetPosition);
				}
				//m_Seek (this.gameObject, m_AIdata);
				if (AIBehavior.OBS (this.gameObject, m_AIData, false) == false) { //如果沒有撞到東西
					if (AIBehavior.Seek (this.gameObject, m_AIData) == false) {
						m_AIData.iAstarIndex = -1;
					}
				}
				AIBehavior.MoveForward (this.gameObject, m_AIData);
				if (fDist < m_AIData.fAttackLength) { //如果距離小於攻擊範圍，切到攻擊狀態
					ChangeState (eNpcState.Attack);
				} else { //如果沒有小於攻擊範圍，就追逐
					ChangeState (eNpcState.Chase);
				}
				return;
			} else {
				Debug.Log("開始A*");
				if (m_AStar.AStarSearch (m_AStar.transform.position, m_AIData.targetPosition)) { //如果傳給Astar的結果為true，iAstarIndex設為0
					m_AIData.iAstarIndex = 0;
					Debug.Log("有A*");
				}
			}

		}



		/*
		if (m_AIData.targetPoint != null) {
			Debug.Log("有目標");
			//Seek到目標
			m_AIData.targetPosition = m_AIData.targetPoint.transform.position;
			//m_Seek (this.gameObject, m_AIdata);
			if (AIBehavior.OBS (this.gameObject, m_AIData, false) == false) { //如果沒有撞到東西
				if (AIBehavior.Seek (this.gameObject, m_AIData) == false) {

				}
			}
			AIBehavior.MoveForward (this.gameObject, m_AIData);
			if (fDist < m_AIData.fAttackLength) { //如果距離小於攻擊範圍，切到攻擊狀態
				ChangeState (eNpcState.Attack);
			} else { //如果沒有小於攻擊範圍，就追逐
				ChangeState (eNpcState.Chase);
			}
			return;
		}
		*/

	}


	void DoWander(){
		float fDist = 10000.0f;
		m_AIData.targetPoint = CheckPlayer (out fDist);
		if (m_AIData.targetPoint != null) {
			
			if (fDist < m_AIData.fAttackLength) { //如果距離小於攻擊範圍，切到攻擊狀態
				ChangeState(eNpcState.Attack);
				return;
			} else { //如果沒有小於攻擊範圍，就追逐
				ChangeState(eNpcState.Chase);
				return;
			}
			
		}
		
		//m_Seek (this.gameObject, m_AIdata);
		if (MoveToTarget ()) {
			ChangeState(eNpcState.Idel);
		}
	}

	void DoChase(){
		float fDist = 10000.0f;
		m_AIData.targetPoint = CheckPlayer (out fDist);
		if (m_AIData.targetPoint != null) {
			
			if (fDist < m_AIData.fAttackLength) { //如果距離小於攻擊範圍，切到攻擊狀態
				ChangeState(eNpcState.Attack);
				return;
			}
		} else{ //回到Idel狀態
			ChangeState(eNpcState.Idel);
			return;
		}
		
		//追逐敵人
		m_AIData.targetPosition = m_AIData.targetPoint.transform.position;
		MoveToTarget ();
	}

	void DoAttack(){
		float fDist = 10000.0f;
		m_AIData.targetPoint = CheckPlayer (out fDist);
		if (m_AIData.targetPoint != null) {
			
			if (fDist >= m_AIData.fAttackLength) { //如果距離大於攻擊範圍，切到追逐狀態
				ChangeState(eNpcState.Chase);
				return;
			}
		} else{ //回到Idel狀態
			ChangeState(eNpcState.Idel);
			return;
		}
		
		Color r = Color.black;
		r.r = Random.Range(0.0f, 1.0f);
		r.g = Random.Range(0.0f, 1.0f);
		r.b = Random.Range(0.0f, 1.0f);
		this.GetComponent<Renderer>().material.color = r;
		if (fTime > fAttackTime) { 
			ChangeState(eNpcState.Idel);
			return;
		}
		fTime += Time.deltaTime;
	}

	//尋找目標
	GameObject CheckPlayer(out float fOutMinDist){ //(裡面回傳距離，判定距離過小時直接進行攻擊等等)

		GameObject [] gos = SceneManager.m_Instance.m_EnemyTarget;
		Vector3 tVec;
		float fDist = 0.0f;
		float fMinDist = 10000.0f;
		GameObject go = null;

		int iLength = SceneManager.m_Instance.m_Target.Length;
		for (int i=0; i<iLength; i++) {
			tVec = gos[i].transform.position-this.transform.position;

			fDist = tVec.magnitude;
			//如果目前距離>可視範圍就無視
			//if(fDist > m_AIData.fDetectLength){
			//	continue;
			//}

			//如果要做背面的敵人就無視，可以在這邊先tVec.Normalize()，再Vector3.Dot(自己面向,tVec)
			//如果Dot<0.0f就continue(0.0f可以根據可視角度作調整)

			//如果目前距離<最小距離就更新最小距離
			if(fDist < fMinDist){
				fMinDist = fDist;
				go = gos[i];
			}
		}
		fOutMinDist = fMinDist;
		return go;
	}

	void OnDrawGizmos(){


		if (m_AIData != null) {

			Gizmos.color = Color.green;
			Gizmos.DrawLine (this.transform.position, this.transform.position + this.transform.forward * m_AIData.fColProbe);
			
			Gizmos.color = Color.blue;
			
			Vector3 vR = this.transform.position + this.transform.right * 1.0f;
			Gizmos.DrawLine (this.transform.position, vR);
			Gizmos.DrawLine (vR, vR + this.transform.forward * m_AIData.fColProbe);
			
			
			Vector3 vL = this.transform.position + this.transform.right * -1.0f;
			Gizmos.DrawLine (this.transform.position, vL);
			Gizmos.DrawLine (vL, vL + this.transform.forward * m_AIData.fColProbe);
			
			Gizmos.DrawLine (vL + this.transform.forward * m_AIData.fColProbe, vR + this.transform.forward * m_AIData.fColProbe);
		
		}

	}
}