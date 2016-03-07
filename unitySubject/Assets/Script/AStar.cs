using UnityEngine;
using System.Collections;

public class AStar : MonoBehaviour {
	
	ArrayList m_OpenList = new ArrayList(); //已生成未考察節點
	ArrayList m_CloseList = new ArrayList();//已確認要訪問的節點
	ArrayList m_PathList = new ArrayList(); //Astar路徑
	
	// Use this for initialization
	void Start () {
		//初始化
		m_OpenList.Clear ();
		m_CloseList.Clear ();
		m_PathList.Clear ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	//回傳生成的Astar的List
	public ArrayList GetPathList() {
		return m_PathList;
	}
	
	//回傳生成的Astar的List的大小
	public int GetPathPointNumber() {
		return m_PathList.Count;
	}
	
	public bool GetAStarSeekPoint(AIData data, ref int currentIndex,ref Vector3 rPos){ //目前所在的index,
		int iLength = m_PathList.Count;
		Vector3 vPos;
		//從終點抓回去
		for(int i = iLength - 1; i >= currentIndex; i--) {
			vPos = (Vector3)m_PathList[i];
			bool bCol = Physics.Linecast(data.thisPoint.transform.position, vPos, 1 << LayerMask.NameToLayer("Obstacle"));
			//bool bCol = Physics.Linecast(tPos, vPos, 1 << LayerMask.NameToLayer("Obstacle"));
			if(bCol == false) { //如果沒有撞到牆壁代表可走
				currentIndex = i;
				rPos = vPos;
				return true;
			}
		}
		return false;
	}
	
	public bool AStarSearch(Vector3 StartPos,Vector3 EndPos){
		
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
		
		//防呆
		if (iStartNode == iEndNode) {
			BuildPath (StartPos, EndPos, pStartNode, pEndNode);
			return true;
		}
		
		//初始化
		m_OpenList.Clear ();
		m_CloseList.Clear ();
		pNodeList [iStartNode].fF = 0.0f;
		pNodeList [iStartNode].fG = 0.0f;
		pNodeList [iStartNode].fH = 0.0f;
		pNodeList [iStartNode].tParent = null;
		
		PathNode currentNode;
		int iNeibors;
		PathNode neiborNode;
		Vector3 tVec = Vector3.zero;
		
		//首先把起點加到m_OpenList
		m_OpenList.Add (pNodeList [iStartNode]);
		
		//m_OpenList還有東西就一直迴圈
		while (m_OpenList.Count != null) {
			currentNode = PopMinNode ();
			
			//防呆
			if (currentNode == null) {
				return false;
			}
			//已經抵達終點
			else if (currentNode.iID == iEndNode) {
				BuildPath (StartPos, EndPos, pStartNode, pEndNode);
				return true;
			}
			
			//找該點有幾個鄰居
			iNeibors = currentNode.iNeibors;
			//比較鄰居
			for (int i=0; i<iNeibors; i++) {
				neiborNode = currentNode.NeiborsNode [i];
				
				//如果該鄰居在m_CloseList就跳過
				if (CheckCloseList (neiborNode)) {
					continue;
				} 
				//如果在m_OpenList裡面，重新計算G值
				else if (CheckOpenList (neiborNode)) {
					tVec = currentNode.tPoint - neiborNode.tPoint; //目前走的
					float fNewG = neiborNode.tParent.fG + tVec.magnitude;
					//新的G值是不是比原本的G值小
					if (fNewG < neiborNode.fG) {
						neiborNode.fG = fNewG;
						neiborNode.fF = neiborNode.fG + neiborNode.fH;
						neiborNode.tParent = currentNode;
					}
					continue;
				}
				
				//如果都沒有就要塞進m_OpenList裡
				tVec = currentNode.tPoint - neiborNode.tPoint;
				neiborNode.tParent = currentNode;
				float fG = neiborNode.tParent.fG + tVec.magnitude;
				tVec = pEndNode.tPoint - neiborNode.tPoint;
				float fH = tVec.magnitude;
				neiborNode.fF = fG + fH;
				neiborNode.fG = fG;
				neiborNode.fH = fH;
				m_OpenList.Add (neiborNode);
			}
			m_CloseList.Add (currentNode); //把已經訪問過的節點存進去
		}
		return false;
	}
	
	void BuildPath(Vector3 vSPos, Vector3 vEPos, PathNode StartNode, PathNode GoalNode) //起點，終點，起始Node，結束Node
	{
		m_PathList.Clear(); //把上一次的Path清掉
		m_PathList.Add(vSPos);
		m_PathList.Add(vEPos);
		PathNode currentNode = GoalNode;
		
		while(currentNode.tParent != null) //因為是從終點算回去，如果還有爸爸，代表還不是起點
		{
			m_PathList.Insert(1, currentNode.tPoint); //在指定的位置插入值，插入vEPos(位置0)和vSPos之間(位置1)
			currentNode = currentNode.tParent; //因為是起點，所以爸爸指給自己，跳出while
		}
		m_PathList.Insert(1, currentNode.tPoint); //在指定的位置插入值，插入vEPos(位置0)和vSPos之間(位置1)
	}
	
	//取得最小的Node，從m_OpenList拿掉
	PathNode PopMinNode(){
		int iLength = m_OpenList.Count;
		float fMin = 10000.0f;
		PathNode pRet = null;
		int index = -1;
		
		for (int i=0; i<iLength; i++) {
			PathNode p = m_OpenList [i] as PathNode;
			if (p.fF < fMin) {
				pRet = p;
				fMin = p.fF;
				index = i;
			}
		}
		if(index>-1){
			m_OpenList.RemoveAt(index);
		}
		return pRet;
	}
	
	//尋找鄰居是否在m_CloseList
	bool CheckCloseList(PathNode node){
		int iLength = m_CloseList.Count;
		if (iLength == 0) {
			return false;
		}
		for (int i=0; i<iLength; i++) {
			PathNode p = m_CloseList [i] as PathNode;
			if (p == node) {
				return true;
			}
		}
		return false;
	}
	
	//尋找鄰居是否在m_OpenList
	bool CheckOpenList(PathNode node){
		int iLength = m_OpenList.Count;
		if (iLength == 0) {
			return false;
		}
		for (int i=0; i<iLength; i++) {
			PathNode p = m_OpenList [i] as PathNode;
			if (p == node) {
				return true;
			}
		}
		return false;
	}
	
	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		int iLength = m_PathList.Count;
		if (iLength > 0) {
			for (int i=0; i<iLength-1; i++) {
				Vector3 vPos = (Vector3)m_PathList [i];
				Vector3 vPosN = (Vector3)m_PathList [i + 1];
				vPos.y += 5.0f;
				vPosN.y += 5.0f;
				Gizmos.DrawLine (vPos, vPosN);
			}
		}
	}
	
}