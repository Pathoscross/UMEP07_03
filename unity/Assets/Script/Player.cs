using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	//Instance
	public static Player m_Instance;
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

	// Use this for initialization
	void Start () {
		m_AStar = this.GetComponent<AStar>();;
		//m_AIData初始化
		m_AIData.fspeed = 0.1f;
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
		m_AIData.fLife = 1000.0f;
		m_AIData.fAttack = 10.0f;
		//FSM的設定
		m_FSMManager = new FSMManager ();
		FSMIdleState IdleState = new FSMIdleState ();
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
		m_FSMManager.AddState (IdleState);
		//m_FSMManager.AddState (TrackState);
		//m_FSMManager.AddState (ChaseState);
		//m_FSMManager.AddState (AttackState);
		//m_FSMManager.AddState (WanderState);
		m_AIData.m_State = m_FSMManager;
	}
	
	// Update is called once per frame
	void Update () {
		m_FSMManager.DoState(m_AIData);
	}

	//void OnCollisionEnter(Collision collision) {
		//collision.transform.position
	//}

	void OnDrawGizmos(){

		if (m_AIData != null) {
			///*
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
			//*/
			/*
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.forward * m_AIData.fDetectLength);

			Gizmos.color = Color.red;
			Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.forward * m_AIData.fAttackLength);
			*/
		}

	}
}