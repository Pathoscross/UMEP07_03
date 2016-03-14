using UnityEngine;
using System.Collections;

public class UI_ButtonMsg : MonoBehaviour {

	public GameObject g_SeleInfo;
	public GameObject g_SeleBag;

	//角色資訊
	void OnButtonClick_SeleInfo(){
		g_SeleInfo.SetActive (true);
	}
	//角色資訊_返回
	void OnButtonClick_SeleInfoBack(){
		g_SeleInfo.SetActive (false);
	}
	//背包
	void OnButtonClick_SeleBag(){
		g_SeleBag.SetActive (true);
	}
	//背包_返回
	void OnButtonClick_SeleBagBack(){
		g_SeleBag.SetActive (false);
	}

}
