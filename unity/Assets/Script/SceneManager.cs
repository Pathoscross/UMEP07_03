using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	public Obstacle [] m_Obs = null;

	public static SceneManager m_Instance;

	WayPoint m_WayPoint = null;
	//PathNode[] m_NodeList = null;

	//目標
	public GameObject [] m_Target = null; //玩家目標
	public GameObject [] m_EnemyTarget = null; //敵人的目標

	//要被管理的prefabs
	public Object m_Player;

	int iCount = 0;

	void Awake(){
		//使用自己
		m_Instance = this;
		//WayPoint
		m_WayPoint = new WayPoint ();
		m_WayPoint.Init ();
		LoadPathPoint.LoadPathPointDesc (m_WayPoint.GetNodeList ());
		//Obstacle障礙物
		GameObject [] gos = GameObject.FindGameObjectsWithTag ("Obstacle");
		int iLength = gos.Length;
		m_Obs = new Obstacle[iLength];
		for (int i=0; i<iLength; i++) {
			m_Obs [i] = gos [i].GetComponent<Obstacle> ();
		}

		//生成Prefab
		//玩家prefabs生成，從外部傳遞id，if = 1 ，生成哪個prefabs，下略
		for (int i=0; i<1; i++) {
			ObjectPool.m_Instance.InitObjectsInPool (m_Player, 1);
			iCount += 1;
		}
		//玩家Prefab
		//int iCount = ObjectPool.m_Instance.m_GameObjects [0].Count;
		m_EnemyTarget = new GameObject[iCount];
		for (int i=0; i<iCount; i++) {
			GameObject go = ObjectPool.m_Instance.LoadObjectFromPool (i);
			//到時直接安排位置，不用Random
			Vector3 pos = Vector3.zero;
			pos.x = Random.Range (15.0f, 22.0f);
			pos.y = 1.0f;
			pos.z = Random.Range (10.0f, -10.0f);
			go.transform.position = pos;
			m_EnemyTarget [i] = go;
			//Vector3.right指向-x，left指向x
			go.transform.forward = Vector3.left;
		}
		//目標設定
		//m_Target = GameObject.FindGameObjectsWithTag ("Enemy");  //目標是tag為Enemy的物件
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

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
