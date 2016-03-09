using UnityEngine;
using System.Collections;

public class UI_FloatingBar : MonoBehaviour {
	public GameObject VitalBar = null;
	public GameObject InsVitalBar = null;
	public Camera worldCamera;
	public Camera guiCamera;
	
	void Awake() {
		//呼叫VitalBar，然後實體化
		VitalBar = Resources.Load("Monster_VitalBar") as GameObject;
		InsVitalBar = Instantiate (VitalBar) as GameObject;
	}

	public void LateUpdate() {
		worldCamera = NGUITools.FindCameraForLayer(this.gameObject.layer);//頭頂物件
		guiCamera = NGUITools.FindCameraForLayer(InsVitalBar.gameObject.layer);//2D物件

		Vector3 pos = worldCamera.WorldToViewportPoint(this.transform.position); //先知道"target3D物件"的螢幕位置(空物件)

		if(pos.z < 0.0f) {
			pos = new Vector3(-10000.0f, -10000.0f, -10000.0f); //給予一個很大的值讓他不要在畫面上，也可以做成不顯示
		} else {
			pos = guiCamera.ViewportToWorldPoint(pos); //再把"target3D物件的螢幕位置"投射到3D位置
			pos.z = 0.0f;
		}
		InsVitalBar.transform.position = pos; //最後將該位置指給要擺放的2D物件
	}

	void OnEnable(){
		InsVitalBar.SetActive(true);
	}

	void OnDisable(){
		InsVitalBar.SetActive(false);
	}

}
