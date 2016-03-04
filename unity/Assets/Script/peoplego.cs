using UnityEngine;
using System.Collections;

public class peoplego : MonoBehaviour {
    public Animator Anim;
    public AnimatorStateInfo BS;
   static int Idle = Animator.StringToHash("Base.Layer.BG_Chibi_Idle");
   static int Run = Animator.StringToHash("Base.Layer.BG_Chibi_B_Run");
    static int Attac = Animator.StringToHash("Base.Layer.0G_Chibi_Attack00");
    static int Skill = Animator.StringToHash("Base.Layer.0G_Chibi_Attack00");
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Anim.SetBool("Run", false);
        Anim.SetBool("Attac", false);
        Anim.SetBool("Skill", false);
        if (Input.GetKey(KeyCode.W))
        {            
            Anim.SetBool("Run", true);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            Anim.SetBool("Attac", true);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            Anim.SetBool("Skill", true);
        }
    }
}
