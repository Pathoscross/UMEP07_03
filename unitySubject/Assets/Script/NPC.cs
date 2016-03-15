using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {
	//Instance
	public static NPC m_Instance;
	//AIData
	public AIData m_AIData = new AIData ();
	public GameObject targetPoint;
	//ASTAR
	public float m_fMaxSpeed = 10.0f;
	public AStar m_AStar;
	//FSM
	private FSMManager m_FSMManager;

	//需要外讀的資料
	public string sName = "我是怪物喔";
	public float fHP = 20.0f;
	public float fMP = 50.0f;
	public float fMaxHP = 20.0f;
	public float fMaxMP = 50.0f;
	/*
	//=============================角色動作=============================
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
	//=============================完=============================
	*/

	void Awake(){
		//iNowEgo = eEgo.Idle;
		m_Instance = this;
		//獲得AStar的Component
		//Camera mainCamera = Camera.main;
		//m_AStar = mainCamera.GetComponent<AStar>();
		m_AStar = this.GetComponent<AStar> ();
		//m_AIData初始化
		m_AIData.fspeed = 0.1f;
		m_AIData.fMaxspeed = m_fMaxSpeed;
		m_AIData.frotate = 0.0f;
		m_AIData.fMaxrotate = 10.0f;
		m_AIData.fColProbe = 1.0f;
		m_AIData.thisPoint = this.gameObject;
		m_AIData.targetPoint = targetPoint;
		m_AIData.m_Obs = SceneManager.m_Instance.m_Obs;
		m_AIData.fRadius = 0.5f;
		m_AIData.iAstarIndex = -1;
		m_AIData.targetPosition = Vector3.zero;
		m_AIData.fDetectLength = 10.0f;
		m_AIData.fAttackLength = 5.0f;
		m_AIData.fAttack = 10.0f;
		m_AIData.fSkill = 30.0f;
		m_AIData.fSkillMP = 20.0f;
		m_AIData.iEXPGET = 20;
		m_AIData.fHP = fHP;
		m_AIData.fMP = fMP;
		m_AIData.fMaxHP = fMaxHP;
		m_AIData.fMaxMP = fMaxMP;
		/*
			生成隊長時呼叫自己的小兵，並傳入變數給小兵，指派他的隊長
		*/
		//FSM的設定
		m_FSMManager = new FSMManager ();
		FSMNpcIdleState IdleState = new FSMNpcIdleState ();
		FSMNpcTrackState TrackState = new FSMNpcTrackState ();
		FSMNpcAttackState AttackState = new FSMNpcAttackState ();
		FSMNpcSkillState SkillState = new FSMNpcSkillState ();
		IdleState.AddTransition (eTransitionID.Idle_To_Track, eStateID.Track);
		TrackState.AddTransition (eTransitionID.Track_To_Attack, eStateID.Attack);
		TrackState.AddTransition (eTransitionID.Track_To_Skill, eStateID.Skill);
		AttackState.AddTransition (eTransitionID.Attack_To_Idle, eStateID.Idle);
		AttackState.AddTransition (eTransitionID.Attack_To_Track, eStateID.Track);
		SkillState.AddTransition (eTransitionID.Skill_To_Idle, eStateID.Idle);
		SkillState.AddTransition (eTransitionID.Skill_To_Track, eStateID.Track);
		m_FSMManager.AddState (IdleState);
		m_FSMManager.AddState (TrackState);
		m_FSMManager.AddState (AttackState);
		m_FSMManager.AddState (SkillState);
		m_AIData.m_State = m_FSMManager;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		m_FSMManager.DoState(m_AIData);
		if (m_AIData.fHP == 0.0f) {
			SceneManager.m_Instance.pComponent.m_AIData.iEXP += m_AIData.iEXPGET;
			int iSlotFSM = -1;
			ObjectPool.m_Instance.UnLoadObjectToPool(out iSlotFSM, this.gameObject);
		}
		//測試經驗值
		//if(Input.GetKey(KeyCode.G)){
		//this.m_AIData.fHP = 0.0f;
		//}
		/*
		m_FSMManager.CurrentState ().CheckState (m_AIData);
		m_FSMManager.CurrentState ().DoState (m_AIData);
		*/
		//=============================角色動作=============================
		/*
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
		*/
		//=============================完=============================
	}

	void OnDrawGizmos(){
		
		if (m_AIData != null) {
			Gizmos.color = Color.green;
			Gizmos.DrawLine (this.transform.position, this.transform.position + this.transform.forward * m_AIData.fColProbe);
			
			Gizmos.color = Color.blue;
			
			Vector3 vR = this.transform.position + this.transform.right * 1.0f;
			Gizmos.DrawLine (this.transform.position, vR);
			Gizmos.DrawLine (vR, vR + this.transform.forward * m_AIData.fColProbe);
			
			
			Vector3 vL = this.transform.position + this.transform.right * -1.0f;
			Gizmos.DrawLine (this.transform.position, vL);
			Gizmos.DrawLine (vL, vL + this.transform.forward * m_AIData.fColProbe);
			
			Gizmos.DrawLine (vL + this.transform.forward * m_AIData.fColProbe, vR + this.transform.forward * m_AIData.fColProbe);
		}
		
	}
}