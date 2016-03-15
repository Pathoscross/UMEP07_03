using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	public static SceneManager m_Instance;

	public Obstacle [] m_Obs = null;
	public Wall [] m_Wall = null;
	GameObject [] gos = null;
	int iLength;

	WayPoint m_WayPoint = null;
	PathNode[] m_NodeList = null;

	//目標
	public GameObject [] m_Target = null; //玩家目標
	public GameObject [] m_EnemyTarget = null; //敵人的目標

	//要被管理的prefabs
	public Object m_Player;
	public Object m_Player2;
	public Object m_Enemy;
	GameObject go;
	Vector3 pos;
	int iPlayerCount = 0;
	public int m_iPlayerCount(){ return iPlayerCount; }
	int iEnemyCount = 0;
	public int m_iEnemyCount(){ return iEnemyCount; }
	int iAllEnemyCount = 0;
	NPC nComponent = null;

	//遊戲判斷
	public GameObject m_NowPlayer;
	public Player pComponent;
	public GameObject m_MoniterVitalBar;
	public bool bEnd = false;
	public bool bWinOrLose = false;

	void Awake(){
		//使用自己
		m_Instance = this;
		//WayPoint
		m_WayPoint = new WayPoint ();
		m_WayPoint.Init ();
		LoadPathPoint.LoadPathPointDesc (m_WayPoint.GetNodeList ());
		//牆壁阻擋
		//m_Wall = GameObject.FindGameObjectsWithTag ("Wall");
		gos = GameObject.FindGameObjectsWithTag ("Wall");
		iLength = gos.Length;
		m_Wall = new Wall[iLength];
		for (int i=0; i<iLength; i++) {
			m_Wall [i] = gos [i].GetComponent<Wall> ();
		}

		//Obstacle障礙物
		gos = GameObject.FindGameObjectsWithTag ("Obstacle");
		iLength = gos.Length;
		m_Obs = new Obstacle[iLength];
		for (int i=0; i<iLength; i++) {
			m_Obs [i] = gos [i].GetComponent<Obstacle> ();
		}

		//生成Prefab
		iPlayerCount = 2;
		m_EnemyTarget = new GameObject[iPlayerCount];
		for (int i=0; i<iPlayerCount; i++) {
			if (i == 0){ go = ObjectPool.m_Instance.InitObjectsInPool (m_Player, 1); }
			if (i == 1){ go = ObjectPool.m_Instance.InitObjectsInPool (m_Player2, 1); }
			m_EnemyTarget [i] = go;
		}
		go = ObjectPool.m_Instance.LoadObjectFromPool (0);
		pos = Vector3.zero;
		pos.x = 5.7f;
		pos.y = 4.0f;
		pos.z = -109.0f;
		go.transform.position = pos;
		m_NowPlayer = go.gameObject;
		pComponent = m_NowPlayer.GetComponent <Player> ();

		//生成敵人Prefab
		iEnemyCount = 0;
		for (int i=0; i<1; i++) {
			ObjectPool.m_Instance.InitObjectsInPool (m_Enemy, 1);
			iEnemyCount += 1;
		}
		m_Target = new GameObject[iEnemyCount];
		for (int i=0; i<iEnemyCount; i++) {
			Debug.Log("敵人的"+i);
			go = ObjectPool.m_Instance.LoadObjectFromPool (i+iPlayerCount);
			//到時直接安排位置，不用Random
			pos = Vector3.zero;
			pos.x = 5.0f;
			pos.y = 1.0f;
			pos.z = -95.0f;
			//存Z軸值
			nComponent = go.gameObject.GetComponent<NPC> ();
			nComponent.m_AIData.thisPositionZ = pos.z;
			//存物件到ObjectPool
			go.transform.position = pos;
			go.transform.forward = Vector3.left;
			//目前玩家==================================
		}
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (pComponent.m_AIData.iEXP == pComponent.m_AIData.iArrayEXP[pComponent.m_AIData.iLV]) {
			if(pComponent.m_AIData.iLV != pComponent.m_AIData.iMAXEXP){
				pComponent.m_AIData.iEXP = 0;
				pComponent.m_AIData.iLV += 1;
			}
		}
		//if (pComponent.m_AIData.targetPoint != null) {
			//m_MoniterVitalBar.SetActive (true);
		//} else {
			//m_MoniterVitalBar.SetActive(false);
		//}
		//1.如果敵人全滅
			//bEnd = true
			//bWinOrLose = true
		//如果玩家全滅
			//bEnd = true
		//2.如果bWinOrLose = true，判斷
		////1.如果bWinOrLose = true，勝利
		////2.如果bWinOrLose = false，失敗
		/*
		if(Input.GetKey(KeyCode.G)){
			//記得之後要改變m_NowPlayer
			int iSlot = -1;
			GameObject goTest = ObjectPool.m_Instance.FindNowPlayer ();
			Debug.Log ("目前的玩家物件名稱："+goTest.gameObject.name);
			pos = goTest.gameObject.transform.position;
			ObjectPool.m_Instance.UnLoadObjectToPool(out iSlot, goTest.gameObject);
			go = ObjectPool.m_Instance.LoadObjectFromPool (1);
			go.transform.position = pos;
		}
		*/
	}


	//WayPoint
	void OnDrawGizmos() {
		if (m_WayPoint == null) {
			return;
		}
		PathNode[] pNodeList = m_WayPoint.GetNodeList (); //從PathNode索取m_pNodeList陣列
		if (pNodeList == null) {
			return;
		}
		Vector3 tPoint;
		int inWP = pNodeList.Length;
		for (int i = 0; i < inWP; i++) {
			tPoint = pNodeList [i].tPoint;
			int iLength = pNodeList [i].iNeibors;
			//PathNode p;
			for (int j = 0; j < iLength; j++) {
				Gizmos.color = Color.cyan;
				Gizmos.DrawLine (pNodeList [i].NeiborsNode [j].tPoint, tPoint);
				
			}
		}
	}
}
