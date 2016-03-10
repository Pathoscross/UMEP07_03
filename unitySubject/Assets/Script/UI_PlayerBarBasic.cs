using UnityEngine;
using System.Collections;

public class UI_PlayerBarBasic : MonoBehaviour {

	public UI_VitalBar vbHP;
	public UI_VitalBar vbMP;
	public UILabel label_Name;
	public UILabel label_LV;
	public float HPcurValue;
	public float MaxHPValue;
	public float MPcurValue;
	public float MaxMPValue;
	
	public bool displayText = true;
	
	void Start() {
		UpdateVitalBar();
		label_Name.text = SceneManager.m_Instance.pComponent.sName;
		label_LV.text = "LV."+SceneManager.m_Instance.pComponent.m_AIData.iLV;
	}

	void Update () {
		MaxHPValue = SceneManager.m_Instance.pComponent.m_AIData.fMaxHP;
		HPcurValue = SceneManager.m_Instance.pComponent.m_AIData.fHP;
		if (HPcurValue < 0.0f) { HPcurValue = 0.0f; }
		MaxMPValue = SceneManager.m_Instance.pComponent.m_AIData.fMaxMP;
		MPcurValue = SceneManager.m_Instance.pComponent.m_AIData.fMP;
		if (MPcurValue < 0.0f) { MPcurValue = 0.0f; }
		UpdateVitalBar();
	}
	
	void UpdateVitalBar() {
		if (!displayText) {
			vbHP.UpdateDisplay((float)(HPcurValue / MaxHPValue));
			vbMP.UpdateDisplay((float)(MPcurValue / MaxMPValue));
		}
		else {
			vbHP.UpdateDisplay((float)(HPcurValue / MaxHPValue), HPcurValue + "/" + MaxHPValue);
			vbMP.UpdateDisplay((float)(MPcurValue / MaxMPValue), MPcurValue + "/" + MaxMPValue);
		}
	}
}