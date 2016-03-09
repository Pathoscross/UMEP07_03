using UnityEngine;
using System.Collections;

public class UI_MonisterBarBasic : MonoBehaviour {

	public UI_VitalBar vb;
	public GameObject m_GO = null;
	public UILabel label_Name;
	public float fHPValue;
	public float fMaxHPValue;
	
	public bool displayText = true;
	public bool display = false;

	void Start() {
		UpdateVitalBar();
		if (m_GO != null) {
			label_Name.text = m_GO.GetComponent<NPC> ().sName;
		}
	}
	
	void Update () {
		if (m_GO != null) {
			fMaxHPValue = m_GO.GetComponent<NPC> ().fMaxHP;
			fHPValue = m_GO.GetComponent<NPC> ().fHP;
			if (fHPValue < 0.0f) { fHPValue = 0.0f; }
			UpdateVitalBar ();
		}
	}
	
	void UpdateVitalBar() {
		if (!displayText) {
			vb.UpdateDisplay((float)(fHPValue / fMaxHPValue));
		} else {
			vb.UpdateDisplay((float)(fHPValue / fMaxHPValue), fHPValue + "/" + fMaxHPValue);
		}
	}
}