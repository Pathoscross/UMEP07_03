using UnityEngine;

public class UI_FloatingBar : MonoBehaviour {
	public GameObject VitalBar = null;
	public GameObject InsVitalBar = null;
	public Camera worldCamera;
	public Camera guiCamera;

	static readonly Vector3 s_vHidden = new Vector3(-10000.0f, -10000.0f, -10000.0f);
	
	void Awake() {
		//呼叫VitalBar，然後實體化
		VitalBar = Resources.Load("Monster_VitalBar") as GameObject;
		if (VitalBar == null) {
			Debug.LogError("找不到 Resources/Monster_VitalBar");
			return;
		}
		InsVitalBar = Instantiate (VitalBar) as GameObject;
		//血槽
		InsVitalBar.GetComponent<UI_MonisterBarBasic> ().m_GO = gameObject.transform.parent.gameObject;
	}

	public void LateUpdate() {
		if (InsVitalBar == null) {
			return;
		}
		//相機只在還沒找到（或被刪掉）時才去找，不必每幀搜尋
		if (worldCamera == null) {
			worldCamera = NGUITools.FindCameraForLayer(this.gameObject.layer);//頭頂物件
		}
		if (guiCamera == null) {
			guiCamera = NGUITools.FindCameraForLayer(InsVitalBar.layer);//2D物件
		}
		if (worldCamera == null || guiCamera == null) {
			return;
		}

		Vector3 pos = worldCamera.WorldToViewportPoint(this.transform.position); //先知道"target3D物件"的螢幕位置(空物件)

		if(pos.z < 0.0f) {
			pos = s_vHidden; //在相機背後就移出畫面
		} else {
			pos = guiCamera.ViewportToWorldPoint(pos); //再把"target3D物件的螢幕位置"投射到3D位置
			pos.z = 0.0f;
		}
		InsVitalBar.transform.position = pos; //最後將該位置指給要擺放的2D物件
	}

	void OnEnable(){
		if (InsVitalBar != null) {
			InsVitalBar.SetActive(true);
		}
	}

	void OnDisable(){
		if (InsVitalBar != null) {
			InsVitalBar.SetActive(false);
		}
	}

}
