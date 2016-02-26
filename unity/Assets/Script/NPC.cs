using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {
	//Instance
	public static NPC m_Instance;
	//基本設定
	public float fTime = 0.0f;
	public float fIdelTime = 3.0f;
	public float fAttackTime = 2.0f; //攻擊動作的時間
	//AIData
	public AIData m_AIData = new AIData ();
	public GameObject targetPoint;
	//ASTAR
	public float m_fMaxSpeed = 10.0f;
	public AStar m_AStar;
	//FSM
	private FSMManager m_FSMManager;

	void Awake(){
		m_Instance = this;
	}

	// Use this for initialization
	void Start () {
		//獲得AStar的Component
		//Camera mainCamera = Camera.main;
		//m_AStar = mainCamera.GetComponent<AStar>();
		m_AStar = this.GetComponent<AStar> ();
		//m_AIData初始化
		m_AIData.fspeed = 0.1f;
		m_AIData.fMaxspeed = m_fMaxSpeed;
		m_AIData.frotate = 0.0f;
		m_AIData.fMaxrotate = 10.0f;
		m_AIData.fColProbe = 3.0f;
		m_AIData.thisPoint = this.gameObject;
		m_AIData.targetPoint = targetPoint;
		m_AIData.m_Obs = SceneManager.m_Instance.m_Obs;
		m_AIData.fRadius = 1.0f;
		m_AIData.iAstarIndex = -1;
		m_AIData.targetPosition = Vector3.zero;
		m_AIData.fDetectLength = 20.0f;
		m_AIData.fAttackLength = 10.0f;
		m_AIData.fLife = 50.0f;
		m_AIData.fAttack = 10.0f;
		/*
			生成隊長時呼叫自己的小兵，並傳入變數給小兵，指派他的隊長
		*/
		//FSM的設定
		m_FSMManager = new FSMManager ();
		FSMNpcIdleState IdleState = new FSMNpcIdleState ();
		FSMNpcTrackState TrackState = new FSMNpcTrackState ();
		FSMNpcChaseState ChaseState = new FSMNpcChaseState ();
		FSMNpcAttackState AttackState = new FSMNpcAttackState ();
		IdleState.AddTransition (eTransitionID.Idle_To_Track, eStateID.Track);
		TrackState.AddTransition (eTransitionID.Track_To_Chase, eStateID.Chase);
		TrackState.AddTransition (eTransitionID.Track_To_Attack, eStateID.Attack);
		ChaseState.AddTransition (eTransitionID.Chase_To_Attack, eStateID.Attack);
		ChaseState.AddTransition (eTransitionID.Chase_To_Idle, eStateID.Idle);
		AttackState.AddTransition (eTransitionID.Attack_To_Chase, eStateID.Chase);
		AttackState.AddTransition (eTransitionID.Attack_To_Idle, eStateID.Idle);
		m_FSMManager.AddState (IdleState);
		m_FSMManager.AddState (TrackState);
		m_FSMManager.AddState (ChaseState);
		m_FSMManager.AddState (AttackState);
		m_AIData.m_State = m_FSMManager;
	}
	
	// Update is called once per frame
	void Update () {
		//m_FSMManager.DoState(m_AIData);
		/*
		m_FSMManager.CurrentState ().CheckState (m_AIData);
		m_FSMManager.CurrentState ().DoState (m_AIData);
		*/
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