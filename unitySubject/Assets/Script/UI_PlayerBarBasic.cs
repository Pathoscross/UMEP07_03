using UnityEngine;

public class UI_PlayerBarBasic : MonoBehaviour {

	public UI_VitalBar vbHP;
	public UI_VitalBar vbMP;
	public UI_VitalBar vbEXP;
	public UILabel label_Name;
	public UILabel label_LV;
	public UILabel label_EXP;
	public float HPcurValue;
	public float MaxHPValue;
	public float MPcurValue;
	public float MaxMPValue;
	public float EXPcurValue;
	public float MaxEXPValue;
	int iLV = -1;
	int iEXP = -1;
	
	public bool displayText = true;
	bool m_bDirty = true; //數值有變才重畫，避免每幀產生新字串
	
	void Start() {
		//名字
		label_Name.text = SceneManager.m_Instance.pComponent.sName;
		Refresh ();
	}

	void Update () {
		Refresh ();
	}

	void Refresh() {
		AIData d = SceneManager.m_Instance.pComponent.m_AIData;

		//等級／經驗值標籤（舊版只在 Start 設一次，升級後不會更新）
		if (d.iLV != iLV) {
			iLV = d.iLV;
			label_LV.text = "LV." + iLV;
			m_bDirty = true;
		}
		if (vbEXP != null && d.iEXP != iEXP) {
			iEXP = d.iEXP;
			label_EXP.text = iEXP.ToString ();
		}

		float fHP = Mathf.Max (d.fHP, 0.0f);
		float fMP = Mathf.Max (d.fMP, 0.0f);
		float fEXP = d.iEXP;
		//經驗表索引防呆，避免滿等時越界
		float fMaxEXP = d.iArrayEXP[Mathf.Clamp (d.iLV, 0, d.iArrayEXP.Length - 1)];

		if (m_bDirty || fHP != HPcurValue || d.fMaxHP != MaxHPValue
			|| fMP != MPcurValue || d.fMaxMP != MaxMPValue
			|| fEXP != EXPcurValue || fMaxEXP != MaxEXPValue) {
			HPcurValue = fHP;
			MaxHPValue = d.fMaxHP;
			MPcurValue = fMP;
			MaxMPValue = d.fMaxMP;
			EXPcurValue = fEXP;
			MaxEXPValue = fMaxEXP;
			UpdateVitalBar ();
		}
	}
	
	void UpdateVitalBar() {
		m_bDirty = false;
		if (!displayText) {
			vbHP.UpdateDisplay(HPcurValue / MaxHPValue);
			vbMP.UpdateDisplay(MPcurValue / MaxMPValue);
			if (vbEXP != null) {
				vbEXP.UpdateDisplay(EXPcurValue / MaxEXPValue);
			}
		}
		else {
			vbHP.UpdateDisplay(HPcurValue / MaxHPValue, HPcurValue + "/" + MaxHPValue);
			vbMP.UpdateDisplay(MPcurValue / MaxMPValue, MPcurValue + "/" + MaxMPValue);
			if (vbEXP != null) {
				vbEXP.UpdateDisplay(EXPcurValue / MaxEXPValue, EXPcurValue + "/" + MaxEXPValue);
			}
		}
	}
}
