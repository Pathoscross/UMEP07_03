using UnityEngine;

public class UI_MonisterBarBasic : MonoBehaviour {

	public UI_VitalBar vb;
	public GameObject m_GO = null;
	public UILabel label_Name;
	public float fHPValue;
	public float fMaxHPValue;
	
	public bool displayText = true;
	public bool display = false;

	NPC m_NPC = null;
	bool m_bDirty = true; //數值有變才重畫，避免每幀產生新字串

	void Start() {
		CacheNPC ();
		if (m_NPC != null) {
			label_Name.text = m_NPC.sName;
		}
		UpdateVitalBar();
	}

	void CacheNPC() {
		if (m_GO != null && (m_NPC == null || m_NPC.gameObject != m_GO)) {
			m_NPC = m_GO.GetComponent<NPC> ();
			m_bDirty = true;
		}
	}
	
	void Update () {
		CacheNPC ();
		if (m_NPC == null) {
			return;
		}
		float fMax = m_NPC.m_AIData.fMaxHP;
		float fHP = Mathf.Max (m_NPC.m_AIData.fHP, 0.0f);
		if (m_bDirty || fMax != fMaxHPValue || fHP != fHPValue) {
			fMaxHPValue = fMax;
			fHPValue = fHP;
			UpdateVitalBar ();
		}
	}
	
	void UpdateVitalBar() {
		m_bDirty = false;
		if (!displayText) {
			vb.UpdateDisplay(fHPValue / fMaxHPValue);
		} else {
			vb.UpdateDisplay(fHPValue / fMaxHPValue, fHPValue + "/" + fMaxHPValue);
		}
	}
}
