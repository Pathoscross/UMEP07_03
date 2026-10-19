using UnityEngine;
using System.Collections;

public class PlayerBoy : MonoBehaviour
{
    //Instance
    public static PlayerBoy m_Instance;
    //基本設定
    public float fTime = 0.0f;
    public float fIdelTime = 3.0f;
    public float fAttackTime = 2.0f; //攻擊動作的時間
                                     //AIData
    public AIData m_AIData = new AIData();
    public GameObject targetPoint;
    //ASTAR
    public float m_fMaxSpeed = 10.0f;
    public AStar m_AStar;
    //FSM
    private FSMManager m_FSMManager;
    //=============================角色動作=============================
    public Animator Anim;
    public AnimatorStateInfo BS;
    static int Idle = Animator.StringToHash("Base.Layer.BW_Chibi_Idle");
    static int Run = Animator.StringToHash("Base.Layer.BW_Chibi_B_Run");
    static int Attac = Animator.StringToHash("Base.Layer.BW_Chibi_Attack00");
    static int Skill = Animator.StringToHash("Base.Layer.BW_Chibi_Skill");
    //Animator 參數的 hash，避免每幀用字串查詢
    static readonly int s_iParamIdle = Animator.StringToHash("Idle");
    static readonly int s_iParamRun = Animator.StringToHash("Run");
    static readonly int s_iParamAttac = Animator.StringToHash("Attac");
    static readonly int s_iParamSkill = Animator.StringToHash("Skill");
    public enum eEgo
    {
        None = -1,
        Idle,
        Run,
        Attac,
        Skill
    }
    public eEgo iNowEgo = eEgo.None;
    //=============================完=============================
    void Awake()
    {
        m_Instance = this;
        iNowEgo = eEgo.Idle;
    }
    // Use this for initialization
    void Start()
    {
        m_AStar = this.GetComponent<AStar>();
        //m_AIData初始化
        m_AIData.fspeed = 20.0f;
        m_AIData.fMaxspeed = m_fMaxSpeed;
        m_AIData.frotate = 0.0f;
        m_AIData.fMaxrotate = 10.0f;
        m_AIData.fColProbe = 2.0f;
        m_AIData.thisPoint = this.gameObject;
        m_AIData.targetPoint = targetPoint;
        m_AIData.m_Obs = SceneManager.m_Instance.m_Obs;
        m_AIData.m_Wall = SceneManager.m_Instance.m_Wall;
        m_AIData.fRadius = 0.5f;
        m_AIData.iAstarIndex = -1;
        m_AIData.targetPosition = Vector3.zero;
        m_AIData.fDetectLength = 20.0f;
        m_AIData.fAttackLength = 10.0f;
        m_AIData.fHP = 100.0f;
        m_AIData.fMP = 30.0f;
        m_AIData.fAttack = 10.0f;
        m_AIData.fSkill = 30.0f;
        //FSM的設定
        m_FSMManager = new FSMManager();
        FSMIdleState IdleState = new FSMIdleState();
        //FSMTrackState TrackState = new FSMTrackState ();
        //FSMChaseState ChaseState = new FSMChaseState ();
        //FSMAttackState AttackState = new FSMAttackState ();
        //FSMWanderState WanderState = new FSMWanderState ();
        //IdleState.AddTransition (eTransitionID.Idle_To_Track, eStateID.Track);
        //IdleState.AddTransition (eTransitionID.Idle_To_Chase, eStateID.Chase);
        //IdleState.AddTransition (eTransitionID.Idle_To_Attack, eStateID.Attack);
        //IdleState.AddTransition (eTransitionID.Idle_To_Wander, eStateID.Wander);
        //TrackState.AddTransition (eTransitionID.Track_To_Idle, eStateID.Idle);
        //TrackState.AddTransition (eTransitionID.Track_To_Chase, eStateID.Chase);
        //TrackState.AddTransition (eTransitionID.Track_To_Attack, eStateID.Attack);
        //TrackState.AddTransition (eTransitionID.Track_To_Wander, eStateID.Wander);
        //ChaseState.AddTransition (eTransitionID.Chase_To_Attack, eStateID.Attack);
        //ChaseState.AddTransition (eTransitionID.Chase_To_Idle, eStateID.Idle);
        //AttackState.AddTransition (eTransitionID.Attack_To_Idle, eStateID.Idle);
        //AttackState.AddTransition (eTransitionID.Attack_To_Chase, eStateID.Chase);
        //WanderState.AddTransition (eTransitionID.Wander_To_Idle, eStateID.Idle);
        m_FSMManager.AddState(IdleState);
        //m_FSMManager.AddState (TrackState);
        //m_FSMManager.AddState (ChaseState);
        //m_FSMManager.AddState (AttackState);
        //m_FSMManager.AddState (WanderState);
        m_AIData.m_State = m_FSMManager;
    }

    // Update is called once per frame
    void Update()
    {
        m_FSMManager.DoState(m_AIData);
        //=============================角色動作=============================
        //四個狀態互斥：只有目前的那個為 true（舊版 Idle 設成 true 後永遠不會關掉）
        Anim.SetBool(s_iParamIdle, iNowEgo == eEgo.Idle);
        Anim.SetBool(s_iParamRun, iNowEgo == eEgo.Run);
        Anim.SetBool(s_iParamAttac, iNowEgo == eEgo.Attac);
        Anim.SetBool(s_iParamSkill, iNowEgo == eEgo.Skill);
        //=============================完=============================
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.name == "_scene_end_limit")
        {
            Debug.Log("==============================碰到出口===================");
            Debug.Log("==============================獲勝，遊戲結束===================");
            SceneManager.m_Instance.bEnd = true;
            SceneManager.m_Instance.bWinOrLose = true;
        }
    }

    void OnDrawGizmos()
    {
        if (m_AIData != null)
        {
            AIGizmos.DrawProbe(this.transform, m_AIData.fColProbe, 1.0f);
        }
    }
}