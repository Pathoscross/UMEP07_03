using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

	//資料結構管理，一個物件和目前是否被使用
	public class ObjectPoolData
	{
		public GameObject m_go;
		public bool m_bUsing;
		public int m_iSlot; //屬於哪個Slot
	}
	public int m_iCount;

	//單例：ObjectPool.m_Instance 即可取用
	public static ObjectPool m_Instance;

	//每個Slot一個List，存同一種prefab生出的物件
	public List<ObjectPoolData>[] m_GameObjects;

	//GameObject -> 資料，查詢從 O(全部物件) 降到 O(1)
	Dictionary<GameObject, ObjectPoolData> m_Lookup = new Dictionary<GameObject, ObjectPoolData> ();

	//初始化時設定Type(就是Slot)的大小
	public int m_iNumGameObjectInType;

	void Awake () {
		m_Instance = this;
		m_iCount = 0;
		m_iNumGameObjectInType = 10; //設定Type(就是Slot)的大小
		m_GameObjects = new List<ObjectPoolData>[m_iNumGameObjectInType];
	}

	int FindEmptySlot()
	{
		for (int i = 0; i < m_iNumGameObjectInType; i++) {
			if (m_GameObjects[i] == null) {
				return i;
			}
		}
		return -1;
	}

	//解構，把所有物件Destroy掉
	public void DeInit()
	{
		for (int i = 0; i < m_iNumGameObjectInType; i++) {
			DestroyPoolSlot (i);
		}
		m_Lookup.Clear ();
	}

	public GameObject InitObjectsInPool(Object obj, int iCount)
	{
		int iSlot = FindEmptySlot();
		GameObject go = null;
		if (iSlot < 0) {
			Debug.LogError ("ObjectPool 的 Slot 已滿（上限 " + m_iNumGameObjectInType + "）");
			return go;
		}
		m_iCount = iCount;
		m_GameObjects[iSlot] = new List<ObjectPoolData>(iCount);
		for (int i = 0; i < iCount; i++) {
			go = Instantiate(obj) as GameObject;
			if (go == null) {
				break;
			}
			EnableModel(go, false); //不顯示建立的物件
			ObjectPoolData objData = new ObjectPoolData();
			objData.m_go = go;
			objData.m_bUsing = false;
			objData.m_iSlot = iSlot;
			m_GameObjects[iSlot].Add(objData);
			m_Lookup[go] = objData;
		}
		return go;
	}

	public GameObject LoadObjectFromPool(int iSlot)
	{
		//Slot超出範圍防呆
		if (iSlot < 0 || iSlot >= m_iNumGameObjectInType || m_GameObjects[iSlot] == null) {
			return null;
		}

		//尋找還沒被使用的物件，有的話啟用並return
		List<ObjectPoolData> list = m_GameObjects[iSlot];
		for (int i = 0; i < list.Count; i++) {
			ObjectPoolData objData = list[i];
			if (!objData.m_bUsing) {
				objData.m_bUsing = true;
				EnableModel(objData.m_go, true);
				return objData.m_go;
			}
		}
		return null;
	}

	//把物件還回池子（關掉）
	public bool UnLoadObjectToPool(out int ioutSlotOP, GameObject go)
	{
		ObjectPoolData objData;
		if (go == null || !m_Lookup.TryGetValue (go, out objData)) {
			ioutSlotOP = -1;
			return false;
		}
		ioutSlotOP = objData.m_iSlot;
		objData.m_bUsing = false;
		EnableModel(go, false);
		return true;
	}

	//物件是否屬於池子且正在使用中
	public bool FindObjectToPool(out int ioutSlotOP, GameObject go) {
		ObjectPoolData objData;
		if (go == null || !m_Lookup.TryGetValue (go, out objData)) {
			ioutSlotOP = -1;
			return false;
		}
		ioutSlotOP = objData.m_iSlot;
		return objData.m_bUsing;
	}

	//找出目前使用中、Tag 為 Player 的物件（多個時回傳最後一個，與舊版相同）
	public GameObject FindNowPlayer() {
		GameObject go = null;
		for (int i = 0; i < m_GameObjects.Length; i++) {
			if (m_GameObjects [i] == null) {
				continue;
			}
			List<ObjectPoolData> list = m_GameObjects [i];
			for (int j = 0; j < list.Count; j++) {
				ObjectPoolData objData = list [j];
				if (objData.m_bUsing && objData.m_go.CompareTag ("Player")) {
					go = objData.m_go;
				}
			}
		}
		return go;
	}

	//針對某一個Slot，把裡面的物件清掉
	public void DestroyPoolSlot(int iSlot)
	{
		if (iSlot < 0 || iSlot >= m_iNumGameObjectInType || m_GameObjects[iSlot] == null) {
			return;
		}
		List<ObjectPoolData> list = m_GameObjects[iSlot];
		for (int i = 0; i < list.Count; i++) {
			m_Lookup.Remove (list[i].m_go);
			Destroy(list[i].m_go);
		}
		m_GameObjects[iSlot] = null;
	}

	public void EnableModel(GameObject go, bool beEnable)
	{
		go.SetActive (beEnable);
	}

	public void ShowModel(GameObject go, bool bShow)
	{
		Renderer [] aRenders = go.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < aRenders.Length; i++) {
			aRenders[i].enabled = bShow;
		}
	}

}
