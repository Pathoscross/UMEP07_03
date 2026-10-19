using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	//Instance
	public static Player m_Instance;
	//AIData
	public AIData m_AIData = new AIData ();
	public GameObject targetPoint;
	//ASTAR
	public float m_fMaxSpeed = 10.0f;
	public AStar m_AStar;
	//FSM
	private FSMManager m_FSMManager;
	//需要外讀的資料
	public string sName = "我是玩家喔";
	public float fHP = 100.0f;
	public float fMP = 50.0f;
	public float fMaxHP = 100.0f;
	public float fMaxMP = 50.0f;

	void Awake(){
		m_Instance = this;
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
		m_AIData.fDetectLength = 10.0f;
		m_AIData.fAttackLength = 5.0f;
		m_AIData.fAttack = 10.0f;
		m_AIData.fSkill = 30.0f;
		m_AIData.iLV = 1;
		m_AIData.iEXP = 0;
		m_AIData.fHP = fHP;
		m_AIData.fMP = fMP;
		m_AIData.fMaxHP = fMaxHP;
		m_AIData.fMaxMP = fMaxMP;
		//FSM的設定
		m_FSMManager = new FSMManager ();
		FSMIdleState IdleState = new FSMIdleState ();
		m_FSMManager.AddState (IdleState);
		m_AIData.m_State = m_FSMManager;
	}
	//註：玩家的 FSM 目前由 PlayerBoy.Update 驅動，這裡不需要 Update

	void OnDrawGizmos(){
		if (m_AIData != null) {
			AIGizmos.DrawProbe (this.transform, m_AIData.fColProbe, 1.0f);
		}
	}
}