using UnityEngine;

public class LoadPathPoint{

	static readonly char[] s_LineSep = new char[] { '\n', '\r' };
	static readonly char[] s_FieldSep = new char[] { ' ', '\t' };

	//讀取 Resources/WayPoint.txt：每行「節點編號 鄰居1 鄰居2 ...」
	public static void LoadPathPointDesc (PathNode [] m_NodeList){

		TextAsset ta = Resources.Load ("WayPoint") as TextAsset;
		if (ta == null) {
			Debug.LogError ("找不到 Resources/WayPoint.txt");
			return;
		}
		string [] sLines = ta.text.Split (s_LineSep, System.StringSplitOptions.RemoveEmptyEntries);

		for (int i = 0; i < sLines.Length; i++) {
			string [] sFields = sLines [i].Trim ().Split (s_FieldSep, System.StringSplitOptions.RemoveEmptyEntries);
			if (sFields.Length < 1) {
				continue; //空行
			}

			int iID;
			if (!TryGetNode (m_NodeList, sFields [0], out iID)) {
				Debug.LogError ("WayPoint.txt 節點編號錯誤：「" + sLines [i].Trim () + "」");
				continue;
			}

			int iNeibor = sFields.Length - 1;
			PathNode node = m_NodeList [iID];
			node.iNeibors = 0;
			node.NeiborsNode = new PathNode[iNeibor];

			for (int j = 0; j < iNeibor; j++) {
				int iNei;
				if (!TryGetNode (m_NodeList, sFields [j + 1], out iNei)) {
					Debug.LogError ("WayPoint.txt 鄰居編號錯誤：「" + sLines [i].Trim () + "」中的 " + sFields [j + 1]);
					continue;
				}
				node.NeiborsNode [node.iNeibors++] = m_NodeList [iNei];
			}
		}

	}

	static bool TryGetNode(PathNode [] nodes, string s, out int id){
		return int.TryParse (s, out id) && id >= 0 && id < nodes.Length && nodes [id] != null;
	}
}
