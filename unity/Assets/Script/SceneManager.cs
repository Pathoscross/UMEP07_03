using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	public Obstacle [] m_Obs = null;

	public static SceneManager m_Instance;

	WayPoint m_WayPoint = null;
	//PathNode[] m_NodeList = null;

	public GameObject [] m_Target = null; //目標

	public GameObject [] m_EnemyTarget = null; //敵人的目標

	void Awake(){
		m_Instance = this;
		m_WayPoint = new WayPoint ();
		m_WayPoint.Init ();
		LoadPathPoint.LoadPathPointDesc (m_WayPoint.GetNodeList());

		GameObject [] gos = GameObject.FindGameObjectsWithTag ("Obstacle");
		int iLength = gos.Length;
		m_Obs = new Obstacle[iLength];
		for (int i=0; i<iLength; i++) {
			m_Obs[i] = gos[i].GetComponent<Obstacle>();
		}

		m_Target = GameObject.FindGameObjectsWithTag ("Enemy");  //目標是tag為Enemy的物件

		m_EnemyTarget = GameObject.FindGameObjectsWithTag ("Player");  //目標是tag為Player的物件

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
