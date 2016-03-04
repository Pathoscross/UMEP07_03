using UnityEngine;
using System.Collections;

public class peoplego : MonoBehaviour {
	
	public static peoplego m_Instance;
    public Animator Anim;
	public AnimatorStateInfo BS;
	static int Idle = Animator.StringToHash("Base.Layer.BG_Chibi_Idle");
	static int Run = Animator.StringToHash("Base.Layer.BG_Chibi_B_Run");
    static int Attac = Animator.StringToHash("Base.Layer.0G_Chibi_Attack00");
    static int Skill = Animator.StringToHash("Base.Layer.0G_Chibi_Attack00");

	public enum eEgo {
		None = -1,
		Idle,
		Run,
		Attac,
		Skill
	}
	public eEgo iNowEgo = eEgo.None;

    // Use this for initialization
    void Start () {
		m_Instance = this;
		iNowEgo = eEgo.Idle;
	}
	
	// Update is called once per frame
	void Update () {
        Anim.SetBool("Run", false);
        Anim.SetBool("Attac", false);
		Anim.SetBool("Skill", false);
		Debug.Log ("iNowEgo======================================"+iNowEgo);
		if(iNowEgo == eEgo.Idle){
			Debug.Log ("iNowEgo目前是idle======================================"+iNowEgo);
			Anim.SetBool("Idle", true);
		} else if(iNowEgo == eEgo.Run){
			Anim.SetBool("Run", true);
		} else if(iNowEgo == eEgo.Attac){
			Anim.SetBool("Attac", true);
		} else if(iNowEgo == eEgo.Skill){
			Anim.SetBool("Skill", true);
		}
         /*
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
        */
    }
}
