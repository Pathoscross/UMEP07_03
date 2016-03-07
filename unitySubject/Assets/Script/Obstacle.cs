using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	public float fObsRadius = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos(){
        Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (this.transform.position, fObsRadius);
    }
}
