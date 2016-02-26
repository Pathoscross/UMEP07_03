using UnityEngine;
using System.Collections;

public class scene_limit : MonoBehaviour {

	public float fLine = 2.5f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawLine (this.transform.position, this.transform.position + this.transform.forward * fLine);
		
		Gizmos.color = Color.blue;
		
		Vector3 vR = this.transform.position + this.transform.right * 20.0f;
		Gizmos.DrawLine (this.transform.position, vR);
		Gizmos.DrawLine (vR, vR + this.transform.forward * fLine);
		
		
		Vector3 vL = this.transform.position + this.transform.right * -20.0f;
		Gizmos.DrawLine (this.transform.position, vL);
		Gizmos.DrawLine (vL, vL + this.transform.forward * fLine);
		
		Gizmos.DrawLine (vL + this.transform.forward * fLine, vR + this.transform.forward * fLine);
	}
}
