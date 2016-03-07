using UnityEngine;
using System.Collections;

public class AIData{
	public float fColProbe = 3.0f; //探針長度
	//速度
	public float fspeed = 0.0f;
	public float fMaxspeed = 0.0f;
	//轉向
	public float frotate = 0.0f;
	public float fMaxrotate = 0.0f;
	//被兩個障礙物卡住的停滯時間
	public float fInTrapTime;
	
	public GameObject thisPoint;
	public GameObject targetPoint;
	
	public Obstacle[] m_Obs;
	public Wall[] m_Wall;
	public float fRadius = 1.0f;

	public Vector3 targetPosition; //目標位置
	public Vector3 thisPosition; //自己位置
	public int iAstarIndex; //目前seek到的Astar是第幾個點
	public float thisPositionZ; //自己位置的Z軸

	public float fDetectLength; //可視範圍長度
	public float fAttackLength; //攻擊範圍長度
	public float fHP = 0.0f; //生命值
	public float fMP = 0.0f; //生命值
	public float fMaxHP = 0.0f; //生命值
	public float fMaxMP = 0.0f; //生命值
	public float fAttack = 0.0f; //攻擊力
	public float fSkill = 0.0f; //技能攻擊力
	public float fSkillMP = 0.0f; //技能損MP
	public AIData targetAIData;

	public FSMManager m_State;
}

public class AIBehavior{

	public static void Attack(AIData data){
		data.targetAIData.fHP -= data.fAttack;
	}

	public static bool Seek(GameObject go, AIData data){
		
		Vector3 tPos = data.targetPosition;
		Vector3 cPos = go.transform.position;
		Vector3 cFor = go.transform.forward;
		
		Vector3 tVec = tPos - cPos;
		tVec.y = 0;
		float dist = tVec.magnitude;
		if (dist <= data.fspeed * Time.deltaTime) {
			go.transform.position = tPos;
			data.fspeed = 0.0f;
			return false;
		}
		
		data.fspeed += 0.1f;
		if (data.fspeed > data.fMaxspeed) {
			data.fspeed = data.fMaxspeed;
		} else if (data.fspeed < 0.1f) {
			data.fspeed = 0.1f;
		}
		
		//go.transform.position += cFor * data.fspeed * Time.deltaTime;

		tVec.Normalize ();

		go.transform.forward = tVec;

		float fDot = Vector3.Dot (tVec, cFor);
		if (fDot < -1.0f) {
			fDot = -1.0f;
		} else if (fDot > 1.0f) {
			fDot = 1.0f;
		}
		
		float fTheta = Mathf.Acos (fDot);
		float fTurnForce = Mathf.Sin (fTheta);
		float fDegree = fTurnForce * Mathf.Rad2Deg;
		
		if (fDegree <= data.fMaxrotate) {
			go.transform.forward = tVec;
		} else {
			
			float fSign = fTurnForce;
			
			Vector3 vTurn = Vector3.Cross (cFor, tVec);
			if (vTurn.y < 0) {
				fSign = -fTurnForce;
			}
			
			if (data.frotate * fSign < 0) {
				data.frotate = 0.0f;
			}
			
			data.frotate += fSign * 0.1f;
			if (data.frotate < 0.0f) {
				if (data.frotate < -data.fMaxrotate) {
					data.frotate = -data.fMaxrotate;
				}
			}
			if (data.frotate > 0.0f) {
				if (data.frotate > data.fMaxrotate) {
					data.frotate = data.fMaxrotate;
				}
			}
			go.transform.Rotate (0.0f, data.frotate, 0.0f);
		}
		
		//如果有撞到東西，保持原本的forward
		if (AIBehavior.OBS (go, data, true)) {
			go.transform.forward = cFor;
		}
		return true;
		
	}
	
	public static void MoveForward(GameObject go, AIData data)
	{
		Vector3 cFor = go.transform.position + go.transform.forward * data.fspeed * Time.deltaTime;
		//go.transform.position = vNPos;
		go.transform.position = cFor;
	}
	
	public static bool OBS(GameObject go, AIData data, bool bTest){
		
		Obstacle [] obs = data.m_Obs;
		int iLength = obs.Length;
		
		Vector3 tPos;
		Vector3 tVec;
		Vector3 cPos = go.transform.position;
		Vector3 cFor = go.transform.forward;
		float fDist = 0.0f;;
		float fTotalR = 0.0f;
		
		float fDot;
		
		float fTheta;
		float fSinLen = 0.0f;
		
		Obstacle mMinObs = null;
		float fMinDist = 10000.0f; //設一個值用來比較目前最小距離
		float fMinDot = 0.0f;
		Vector3 tMinVec = Vector3.zero;
		float fMinTotalR = 0.0f;
		
		for (int i=0; i<iLength; i++) {
			
			tPos = obs [i].transform.position;
			tVec = tPos - cPos;
			fDist = tVec.magnitude;
			fTotalR = data.fRadius + obs [i].fObsRadius;
			
			//兩者距離>探針長度+兩者的radius，跳過
			if (fDist > data.fColProbe + fTotalR) {
				continue;
			}
			
			//Dot<0，在背面，跳過
			tVec.Normalize ();
			fDot = Vector3.Dot (cFor, tVec);
			if (fDot < 0.01f) {
				continue;
			} else if (fDot > 1.0f) {
				fDot = 1.0f;
			}
			
			//sin距離>兩者的radius，跳過
			fTheta = Mathf.Acos(fDot);
			fSinLen = fDist*Mathf.Sin(fTheta);
			if(fSinLen > fTotalR){
				continue;
			}
			
			if(bTest){
				return true;
			}
			
			if(fDist < fMinDist){
				mMinObs = obs[i];
				fMinDist = fDist;
				fMinDot = fDot;
				tMinVec = tVec;
				fMinTotalR = fTotalR;
			}
		}
		
		//如果mMinObs不是null就轉向
		if (mMinObs != null) {
			
			float fForwardForce = fMinDot;
			float fTurnForce = fMinTotalR/(fSinLen+0.01f);
			
			float fSign = fTurnForce * 0.1f;
			
			Vector3 vTurn = Vector3.Cross (cFor, tMinVec);
			if (vTurn.y > 0) {
				fSign = -fTurnForce * 0.1f;
			}
			
			if (data.frotate * fSign < 0) {
				data.frotate = 0.0f;
			}
			
			data.frotate += fSign * 0.1f;
			if (data.frotate < 0.0f) {
				if (data.frotate < -data.fMaxrotate) {
					data.frotate = -data.fMaxrotate;
				}
			}
			if (data.frotate > 0.0f) {
				if (data.frotate > data.fMaxrotate) {
					data.frotate = data.fMaxrotate;
				}
			}

			float fBigForce = 1.0f;
			
			data.fInTrapTime += Time.deltaTime;
			if(data.fInTrapTime > 1.0f) {
				fBigForce += data.fInTrapTime*20.0f;
				data.frotate = fSign;
			}
			
			go.transform.Rotate (0.0f, data.frotate*fBigForce, 0.0f);
			
			data.fspeed -= fForwardForce;
			if(data.fspeed < 0.0f){
				data.fspeed = 0.0f;
			}
			return true;
		}
		data.fInTrapTime = 0.0f;
		return false;
	}

	public static bool OBSWall(GameObject go, AIData data, out Vector3 vForward){
		
		Wall [] obsWall = data.m_Wall;
		int iLength = obsWall.Length;
		
		Vector3 tPos;
		Vector3 tVec;
		Vector3 cPos = go.transform.position;
		Vector3 cFor = go.transform.forward;
		float fDist = 0.0f;;
		float fTotalR = 0.0f;
		
		float fDot;
		
		float fTheta;
		float fSinLen = 0.0f;
		
		Wall mMinObs = null;
		float fMinDist = 10000.0f; //設一個值用來比較目前最小距離
		
		for (int i=0; i<iLength; i++) {
			tPos = obsWall [i].transform.position;
			tVec = tPos - cPos;
			fDist = tVec.magnitude;
			fTotalR = data.fRadius + obsWall [i].fWallColProbe;
			//兩者距離>探針長度+兩者的radius，跳過
			if (fDist > data.fColProbe + fTotalR) {
				continue;
			}
			
			//Dot<0，在背面，跳過
			tVec.Normalize ();
			fDot = Vector3.Dot (cFor, tVec);
			if (fDot < 0.01f) {
				continue;
			} else if (fDot > 1.0f) {
				fDot = 1.0f;
			}
			
			//sin距離>兩者的radius，跳過
			fTheta = Mathf.Acos(fDot);
			fSinLen = fDist*Mathf.Sin(fTheta);
			if(fSinLen > fTotalR){
				continue;
			}
			
			if(fDist < fMinDist){
				mMinObs = obsWall[i];
				fMinDist = fDist;
			}
		}
		
		//如果mMinObs不是null就轉向
		if (mMinObs != null) {
			vForward = mMinObs.transform.forward;
			return true;
		}
		vForward = Vector3.zero;
		return false;
	}

	public static GameObject CheckPlayer(AIData data, out float fOutMinDist, out int ioutSlotAI){ //(裡面回傳距離，判定距離過小時直接進行攻擊等等)
		
		GameObject [] gos = SceneManager.m_Instance.m_EnemyTarget;
		Vector3 tPos;
		Vector3 tVec;
		float fDist = 0.0f;
		float fMinDist = 10000.0f;
		GameObject go = null;
		//int iTarget = -1;
		bool bUsing = false;
		ioutSlotAI = -1;
		int iLength = SceneManager.m_Instance.m_EnemyTarget.Length;
		for (int i=0; i<iLength; i++) {
			tPos = gos [i].transform.position;
			tPos.z = data.thisPositionZ;
			tVec = tPos - data.thisPoint.gameObject.transform.position;
			fDist = tVec.magnitude;
			bUsing = ObjectPool.m_Instance.FindObjectToPool (out ioutSlotAI, gos [i].gameObject);
			//bUsing = ObjectPool.m_Instance.FindObjectToPool(i, gos[i].gameObject);
			Debug.Log ("啟用=" + bUsing);
			if (fDist < fMinDist && bUsing == true) {
				Debug.Log ("找到目標");
				//iTarget = i;
				fMinDist = fDist;
				go = gos [i];
			}
		}
		//ioutTarget = iTarget;
		fOutMinDist = fMinDist;
		return go;
	}
}