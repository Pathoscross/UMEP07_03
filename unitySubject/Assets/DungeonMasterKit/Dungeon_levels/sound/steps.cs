using UnityEngine;
using System.Collections;

public class shagi : MonoBehaviour {
public CharacterController controller;
public AudioClip [] Shagi;
public AudioClip [] ohhahh;
public float Speed;
	IEnumerator Start () {
		while(true)
			{
				if(controller.isGrounded && controller.velocity.magnitude > 2)
			{
					audio.PlayOneShot(Shagi[Random.Range(0, Shagi.Length)], 0.5F);
					yield return new WaitForSeconds(Speed);
			}
				if(controller.isGrounded && controller.velocity.magnitude < 2)
			{
					audio.PlayOneShot(ohhahh[Random.Range(0, ohhahh.Length)], 0.5F);
					yield return new WaitForSeconds(Speed);
			}
				else
			{
					yield return 0;

			}

		}
	}
}