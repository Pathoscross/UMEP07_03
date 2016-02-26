using UnityEngine;
using System.Collections;

public class LoadPathPoint{

	public static void LoadPathPointDesc (PathNode [] m_NodeList){

		TextAsset ta=(TextAsset)Resources.Load ("WP");
		string [] sText=ta.text.Split("\n"[0]);
		int tLenth=sText.Length;
		string sID;
		string [] sText2;
		int iNeibor=0;

		for (int i=0; i<tLenth; i++) {
			sID = sText [i];
			sID = sID.Trim ();

			sText2 = sID.Split (" " [0]);
			if (sText2.Length < 1) {
				continue;
			}

			iNeibor = sText2.Length - 1;
			sID = sText2 [0];
			int iID = int.Parse (sID);

			m_NodeList [iID].iNeibors = iNeibor;
			m_NodeList [iID].NeiborsNode = new PathNode[iNeibor];

			for (int j=0; j<iNeibor; j++) {
				sID = sText2 [j + 1];
				int iNei = int.Parse (sID);
				m_NodeList [i].NeiborsNode [j] = m_NodeList [iNei];
			}
		}

	}
}
