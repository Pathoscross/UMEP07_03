using UnityEngine;
using System.Collections;

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
	int iLV;
	int iEXP;
	
	public bool displayText = true;
	
	void Start() {
		UpdateVitalBar();
		//名字
		label_Name.text = SceneManager.m_Instance.pComponent.sName;
		//等級
		iLV = SceneManager.m_Instance.pComponent.m_AIData.iLV;
		label_LV.text = "LV." + iLV.ToString();
		//經驗值
		iEXP = SceneManager.m_Instance.pComponent.m_AIData.iEXP;
		label_EXP.text = "LV." + iLV.ToString();
	}

	void Update () {
		//血量
		HPcurValue = SceneManager.m_Instance.pComponent.m_AIData.fHP;
		MaxHPValue = SceneManager.m_Instance.pComponent.m_AIData.fMaxHP;
		if (HPcurValue < 0.0f) { HPcurValue = 0.0f; }
		//MP
		MPcurValue = SceneManager.m_Instance.pComponent.m_AIData.fMP;
		MaxMPValue = SceneManager.m_Instance.pComponent.m_AIData.fMaxMP;
		if (MPcurValue < 0.0f) { MPcurValue = 0.0f; }
		//EXP
		if (vbEXP != null) {
			EXPcurValue = SceneManager.m_Instance.pComponent.m_AIData.iEXP;
			MaxEXPValue = SceneManager.m_Instance.pComponent.m_AIData.iArrayEXP[SceneManager.m_Instance.pComponent.m_AIData.iLV];
			if (MPcurValue < 0.0f) { MPcurValue = 0.0f; }
		}
		UpdateVitalBar();
	}
	
	void UpdateVitalBar() {
		if (!displayText) {
			vbHP.UpdateDisplay((float)(HPcurValue / MaxHPValue));
			vbMP.UpdateDisplay((float)(MPcurValue / MaxMPValue));
			if (vbEXP != null) {
				vbEXP.UpdateDisplay((float)(EXPcurValue / MaxEXPValue));
			}
		}
		else {
			vbHP.UpdateDisplay((float)(HPcurValue / MaxHPValue), HPcurValue + "/" + MaxHPValue);
			vbMP.UpdateDisplay((float)(MPcurValue / MaxMPValue), MPcurValue + "/" + MaxMPValue);
			if (vbEXP != null) {
				vbEXP.UpdateDisplay((float)(EXPcurValue / MaxEXPValue), EXPcurValue + "/" + MaxEXPValue);
			}
		}
	}
}