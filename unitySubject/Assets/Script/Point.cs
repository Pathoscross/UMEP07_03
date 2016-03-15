using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireSphere (this.transform.position, 0.5f);
	}
}
