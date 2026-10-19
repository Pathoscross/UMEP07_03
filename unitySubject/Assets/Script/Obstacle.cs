using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	public float fObsRadius = 5.0f;

	

	void OnDrawGizmos(){
        Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (this.transform.position, fObsRadius);
    }
}
