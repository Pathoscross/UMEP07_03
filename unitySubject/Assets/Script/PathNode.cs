using UnityEngine;
using System.Collections;

public class PathNode{
	public Vector3 tPoint;
	public int iNeibors;
	public PathNode [] NeiborsNode;
	//f(n) = g(n) + h(n)
	//g(n): 從啟始點到目前節點的距離
	//h(n): 預測目前節點到結束點的距離(此為 A* 演算法的主要評價公式)
	//f(n): 目前結點的評價分數
	public float fH;
	public float fG;
	public float fF;
	public PathNode tParent;
	public int iID; //WP編號
	public int tNodeState;
}

public class WayPoint{

	PathNode [] m_NodeList = null;

	public static WayPoint m_Instance = null;

	public void Init(){
		m_Instance = this;
		SetupWPNode ();
	}

	//安裝Tag是WP的物件
	void SetupWPNode(){
		GameObject [] point = GameObject.FindGameObjectsWithTag ("WP");
		int pLenth = point.Length;
		m_NodeList = new PathNode[pLenth];
		string tNodeName = "";
		string [] s;
		int iWP;
		
		for (int i = 0; i < pLenth; i++) {
			PathNode pNode = new PathNode ();
			pNode.iNeibors = 0;
			pNode.NeiborsNode = null;
			pNode.fH = 0.0f;
			pNode.fG = 0.0f;
			pNode.fF = 0.0f;
			pNode.tParent = null;
			pNode.tPoint = point [i].transform.position;
			
			tNodeName = point [i].name;
			s = tNodeName.Split ('_');
			
			iWP = int.Parse (s [1]);
			pNode.iID = iWP;
			m_NodeList [iWP] = pNode;
		}
	}

	//回傳NodeList
	public PathNode[] GetNodeList(){
		return m_NodeList;
	}

	//獲得Node的ID
	public int GetNodeID(Vector3 pos){
		int iLength = m_NodeList.Length;
		Vector3 posN;
		float fMinDist = 10000.0f;
		int iRet = -1;

		for (int i = 0; i < iLength; i++) {
			posN = m_NodeList [i].tPoint;
			if (Physics.Linecast (pos, posN, 1 << LayerMask.NameToLayer ("Wall"))) {
				continue;
			}
			Vector3 tVec = posN - pos;
			if (tVec.magnitude < fMinDist) {
				fMinDist = tVec.magnitude;
				iRet = i;
			}
		}
		return iRet;
	}

}
