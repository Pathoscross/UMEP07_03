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

			//名稱格式必須是 XXX_編號，且編號要在範圍內
			if (s.Length < 2 || !int.TryParse (s [1], out iWP) || iWP < 0 || iWP >= pLenth) {
				Debug.LogError ("WayPoint 名稱格式錯誤（需為 XXX_編號，且編號 < " + pLenth + "）：" + tNodeName);
				continue;
			}
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
		int iWallMask = 1 << LayerMask.NameToLayer ("Wall");
		float fMinSqrDist = float.MaxValue;
		int iRet = -1;

		for (int i = 0; i < iLength; i++) {
			if (m_NodeList [i] == null) {
				continue;
			}
			Vector3 posN = m_NodeList [i].tPoint;
			//先比距離（便宜），比較近才做 Linecast（昂貴）
			float fSqrDist = (posN - pos).sqrMagnitude;
			if (fSqrDist >= fMinSqrDist) {
				continue;
			}
			if (Physics.Linecast (pos, posN, iWallMask)) {
				continue;
			}
			fMinSqrDist = fSqrDist;
			iRet = i;
		}
		return iRet;
	}

}
