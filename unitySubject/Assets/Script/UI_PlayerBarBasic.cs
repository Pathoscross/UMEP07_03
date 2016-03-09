using UnityEngine;
using System.Collections;

public class UI_PlayerBarBasic : MonoBehaviour {
	
	public UI_VitalBar vb;
	public float curValue;
	public float maxValue;
	
	public bool displayText = true;
	
	void Start() {
		UpdateVitalBar();
	}

	void Update () {
		maxValue = SceneManager.m_Instance.pComponent.m_AIData.fMaxHP;
		curValue = SceneManager.m_Instance.pComponent.m_AIData.fHP;
		if (curValue < 0.0f) {
			curValue = 0.0f;
		}
		UpdateVitalBar();
	}
	
	void UpdateVitalBar() {
		if (!displayText) {
			vb.UpdateDisplay((float)(curValue / maxValue));
		}
		else {
			vb.UpdateDisplay((float)(curValue / maxValue), curValue + "/" + maxValue);
		}
	}
}