using UnityEngine;
using System.Collections;

public class UI_MonisterBarBasic : MonoBehaviour {

	public UI_VitalBar vb;
	//GameObject m_NowPlayer;
	//Player pComponent;
	GameObject m_NowPlayerTarget;
	NPC nComponent;
	public float curValue;
	public float maxValue;
	
	public bool displayText = true;
	public bool display = false;

	void Start() {
		UpdateVitalBar();
		//m_NowPlayer = ObjectPool.m_Instance.FindNowPlayer ();
		//pComponent = m_NowPlayer.GetComponent <Player> ();
	}
	
	void Update () {
		if (SceneManager.m_Instance.pComponent.m_AIData.targetPoint != null) {
			m_NowPlayerTarget = SceneManager.m_Instance.pComponent.m_AIData.targetPoint;
			nComponent = m_NowPlayerTarget.GetComponent <NPC> ();
			maxValue = nComponent.m_AIData.fMaxHP;
			curValue = nComponent.m_AIData.fHP;
			if (curValue < 0.0f) {
				curValue = 0.0f;
			}
			UpdateVitalBar ();
		}
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