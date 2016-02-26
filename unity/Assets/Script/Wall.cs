using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
	
	public float fWallColProbe = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos(){

		Gizmos.color = Color.green;
		Gizmos.DrawLine (this.transform.position, this.transform.position + this.transform.forward * fWallColProbe);
			
		Gizmos.color = Color.blue;
			
		Vector3 vR = this.transform.position + this.transform.right * 40.0f;
		Gizmos.DrawLine (this.transform.position, vR);
		Gizmos.DrawLine (vR, vR + this.transform.forward * fWallColProbe);
			
			
		Vector3 vL = this.transform.position + this.transform.right * -40.0f;
		Gizmos.DrawLine (this.transform.position, vL);
		Gizmos.DrawLine (vL, vL + this.transform.forward * fWallColProbe);
			
		Gizmos.DrawLine (vL + this.transform.forward * fWallColProbe, vR + this.transform.forward * fWallColProbe);
		/*
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.forward * m_AIData.fDetectLength);

			Gizmos.color = Color.red;
			Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.forward * m_AIData.fAttackLength);
			*/
	}
}
