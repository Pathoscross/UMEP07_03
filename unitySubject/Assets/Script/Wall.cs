using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
	
	public float fWallColProbe = 5.0f;

	

	void OnDrawGizmos(){
		AIGizmos.DrawProbe (this.transform, fWallColProbe, 40.0f);
	}
}
