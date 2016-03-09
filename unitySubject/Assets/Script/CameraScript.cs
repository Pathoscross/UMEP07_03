using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	GameObject go;
	Player pGo;
	public Transform lookAtObj;
	// Update is called once per frame
	void Update () {
		// look at player
		go = ObjectPool.m_Instance.FindNowPlayer ();
		pGo = go.GetComponent<Player> ();
		lookAtObj = pGo.lookAtObj;
		transform.LookAt (lookAtObj);    
	}
}