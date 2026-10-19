using UnityEngine;
using System.Collections.Generic;

public class AStar : MonoBehaviour {

	List<PathNode> m_OpenList = new List<PathNode>();         //已生成未考察節點
	HashSet<PathNode> m_OpenSet = new HashSet<PathNode>();    //同上，用來 O(1) 判斷是否在 OpenList
	HashSet<PathNode> m_CloseSet = new HashSet<PathNode>();   //已確認要訪問的節點
	List<Vector3> m_PathList = new List<Vector3>();           //Astar路徑

	static int s_iObstacleMask = -1;
	static int ObstacleMask {
		get {
			if (s_iObstacleMask == -1) {
				s_iObstacleMask = 1 << LayerMask.NameToLayer ("Obstacle");
			}
			return s_iObstacleMask;
		}
	}

	//回傳生成的Astar的List
	public List<Vector3> GetPathList() {
		return m_PathList;
	}

	//回傳生成的Astar的List的大小
	public int GetPathPointNumber() {
		return m_PathList.Count;
	}

	public bool GetAStarSeekPoint(AIData data, ref int currentIndex, ref Vector3 rPos){ //目前所在的index
		Vector3 cPos = data.thisPoint.transform.position;
		//從終點抓回去，找第一個看得到的點
		for (int i = m_PathList.Count - 1; i >= currentIndex; i--) {
			Vector3 vPos = m_PathList[i];
			if (!Physics.Linecast (cPos, vPos, ObstacleMask)) { //如果沒有撞到障礙代表可走
				currentIndex = i;
				rPos = vPos;
				return true;
			}
		}
		return false;
	}

	public bool AStarSearch(Vector3 StartPos, Vector3 EndPos){

		//尋找距離起始POS最近的node
		int iStartNode = WayPoint.m_Instance.GetNodeID (StartPos);
		//尋找距離終點POS最近的node
		int iEndNode = WayPoint.m_Instance.GetNodeID (EndPos);

		//防呆
		if (iStartNode == -1 || iEndNode == -1) {
			return false;
		}

		PathNode [] pNodeList = WayPoint.m_Instance.GetNodeList (); //獲得所有的節點
		PathNode pStartNode = pNodeList [iStartNode];
		PathNode pEndNode = pNodeList [iEndNode];

		//起點的 parent 一定要清掉，否則 BuildPath 會沿著上一次搜尋殘留的 parent 走
		pStartNode.fF = 0.0f;
		pStartNode.fG = 0.0f;
		pStartNode.fH = 0.0f;
		pStartNode.tParent = null;

		if (iStartNode == iEndNode) {
			BuildPath (StartPos, EndPos, pEndNode);
			return true;
		}

		//初始化
		m_OpenList.Clear ();
		m_OpenSet.Clear ();
		m_CloseSet.Clear ();

		//首先把起點加到m_OpenList
		AddToOpen (pStartNode);

		//m_OpenList還有東西就一直迴圈
		while (m_OpenList.Count > 0) {
			PathNode currentNode = PopMinNode ();

			//已經抵達終點
			if (currentNode == pEndNode) {
				BuildPath (StartPos, EndPos, pEndNode);
				return true;
			}
			m_CloseSet.Add (currentNode); //把已經訪問過的節點存進去

			//比較鄰居
			int iNeibors = currentNode.iNeibors;
			for (int i = 0; i < iNeibors; i++) {
				PathNode neiborNode = currentNode.NeiborsNode [i];

				//如果該鄰居在m_CloseList就跳過
				if (neiborNode == null || m_CloseSet.Contains (neiborNode)) {
					continue;
				}

				//經由 currentNode 走到鄰居的 G 值
				float fNewG = currentNode.fG + Vector3.Distance (currentNode.tPoint, neiborNode.tPoint);

				//如果在m_OpenList裡面，新的G值比較小才更新
				if (m_OpenSet.Contains (neiborNode)) {
					if (fNewG < neiborNode.fG) {
						neiborNode.fG = fNewG;
						neiborNode.fF = neiborNode.fG + neiborNode.fH;
						neiborNode.tParent = currentNode;
					}
					continue;
				}

				//如果都沒有就要塞進m_OpenList裡
				neiborNode.tParent = currentNode;
				neiborNode.fG = fNewG;
				neiborNode.fH = Vector3.Distance (pEndNode.tPoint, neiborNode.tPoint);
				neiborNode.fF = neiborNode.fG + neiborNode.fH;
				AddToOpen (neiborNode);
			}
		}
		return false;
	}

	void AddToOpen(PathNode node){
		m_OpenList.Add (node);
		m_OpenSet.Add (node);
	}

	//起點，終點，結束Node；從終點沿 parent 走回起點
	void BuildPath(Vector3 vSPos, Vector3 vEPos, PathNode GoalNode)
	{
		m_PathList.Clear(); //把上一次的Path清掉
		m_PathList.Add(vSPos);
		m_PathList.Add(vEPos);
		PathNode currentNode = GoalNode;
		while (currentNode != null) {
			m_PathList.Insert(1, currentNode.tPoint); //插在起點(0)之後
			currentNode = currentNode.tParent;
		}
	}

	//取得最小的Node，從m_OpenList拿掉
	PathNode PopMinNode(){
		int iLength = m_OpenList.Count;
		float fMin = float.MaxValue;
		int index = -1;

		for (int i = 0; i < iLength; i++) {
			if (m_OpenList [i].fF < fMin) {
				fMin = m_OpenList [i].fF;
				index = i;
			}
		}
		if (index < 0) {
			return null;
		}
		PathNode pRet = m_OpenList [index];
		//與最後一個交換再移除，避免 RemoveAt 搬動整個陣列
		m_OpenList [index] = m_OpenList [iLength - 1];
		m_OpenList.RemoveAt (iLength - 1);
		m_OpenSet.Remove (pRet);
		return pRet;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Vector3 vUp = Vector3.up;
		for (int i = 0; i < m_PathList.Count - 1; i++) {
			Gizmos.DrawLine (m_PathList [i] + vUp, m_PathList [i + 1] + vUp);
		}
	}

}
