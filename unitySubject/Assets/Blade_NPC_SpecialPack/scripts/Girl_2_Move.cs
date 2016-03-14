using System.Collections;
using UnityEngine;

public class Girl_2_Move : MonoBehaviour
{
    public Animator Anim;
    public AnimatorStateInfo BS;
    private bool x;

    private static int AttacStandy = Animator.StringToHash("Base.Layer.BG_AttackStandy");
    private static int Run = Animator.StringToHash("Base.Layer.BG_Run01");
    private static int L_Run = Animator.StringToHash("Base.Layer.BG_L_Run01");
    private static int R_Run = Animator.StringToHash("Base.Layer.BG_R_Run01");
    private static int B_Run = Animator.StringToHash("Base.Layer.BG_B_Run01");
    private static int Attac = Animator.StringToHash("Base.Layer.BG_Attack00");
    private static int Death = Animator.StringToHash("Base.Layer.BG_Death");

    // Use this for initialization
    private void Start()
    {
        x = true;
    }

    // Update is called once per frame
    private void Update()
    {
        Anim.SetBool("Run", false);
        Anim.SetBool("L_Run", false);
        Anim.SetBool("R_Run", false);
        Anim.SetBool("B_Run", false);
        Anim.SetBool("Attac", false);
        Anim.SetBool("Death", false);

        if (Input.GetKey(KeyCode.W))
        {
            Anim.SetBool("Run", x);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Anim.SetBool("L_Run", x);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Anim.SetBool("R_Run", x);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Anim.SetBool("B_Run", x);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            Anim.SetBool("Attac", x);
        }
    }
}