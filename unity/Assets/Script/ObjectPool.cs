using UnityEngine;
using System.Collections;
using System.Collections.Generic; //要使用List必須

public class ObjectPool : MonoBehaviour {
	
	//資料結構管理，一個物件和目前是否被使用
	public class ObjectPoolData
	{
		public GameObject m_go;
		public bool m_bUsing;
	}
	public int m_iCount;
	
	//因為是static宣告，所以可以ObjectPool.m_Instance.(該script的所有東西做使用)
	//要宣告為exercise的，才能在其他地方使用m_Instance
	//只要初始化時m_Instance=自己即可
	public static ObjectPool m_Instance;
	
	//ObjectPoolData確認建立資料後，資料要存入的LIST
	public List<ObjectPoolData>[] m_GameObjects;
	
	//初始化時設定Type(就是Slot)的大小
	public int m_iNumGameObjectInType;
	
	void Awake () {
		m_Instance = this; //m_Instance=自己
		m_iCount = 0;
		m_iNumGameObjectInType = 10; //設定Type(就是Slot)的大小
		m_GameObjects = new List<ObjectPoolData>[10];
	}
	
	int FindEmptySlot()
	{
		int i;
		for(i = 0; i < m_iNumGameObjectInType; i++) {
			if(m_GameObjects[i] == null) {
				break;	
			}
		}
		if(i == m_iNumGameObjectInType) {
			return -1;
		} else {
			return i;
		}
	}
	
	//解構，如果不是空物件的話就Destory掉
	public void DeInit()
	{
		int i, j;
		int iCount;
		for(i = 0; i < m_iNumGameObjectInType; i++) {
			if(m_GameObjects[i] != null) {
				iCount = m_GameObjects[i].Count;
				for(j = 0; j < iCount; j++) {
					Destroy(m_GameObjects[i][j].m_go);
					Debug.Log("ddd");
				}
			}
		}
	}
	
	public GameObject InitObjectsInPool(Object obj, int iCount)
	{
		int iSlot = FindEmptySlot();
		GameObject go = null;
		// Not found, return or resize buffer.
		if(iSlot < 0) {
			return go;	
		}
		m_iCount = iCount;
		m_GameObjects[iSlot] = new List<ObjectPoolData>();
		for(int i = 0; i < iCount; i++) {
			go = Instantiate(obj) as GameObject;
			if(go == null) {
				break;
			}
			EnableModel(go, false); //不顯示建立的物件
			//將建立一個新的ObjectPoolData，存物件的狀態(開始為false)
			ObjectPoolData objData = new ObjectPoolData();
			objData.m_go = go;
			objData.m_bUsing = false;
			//將第iSlot的m_GameObjects，增加一個建立的物件
			m_GameObjects[iSlot].Add(objData);
		}
		return go;
	}
	
	public GameObject LoadObjectFromPool(int iSlot)
	{
		//Slot超出範圍可能會擋掉，所以防呆
		if(iSlot < 0 || iSlot >= m_iNumGameObjectInType) {
			return null;	
		}
		
		GameObject go = null;
		int iCount = m_GameObjects[iSlot].Count; //這個元素有幾個Count，指回iCount變數
		
		//尋找有無還沒有被使用的物件，有的話啟用並return
		for(int i = 0; i < iCount; i++) {
			ObjectPoolData objData = m_GameObjects[iSlot][i];
			if(objData.m_bUsing == false) {
				go = objData.m_go;
				//go.active = true;
				//ShowModel(go, true);
				EnableModel(go, true);
				objData.m_bUsing = true;
				m_GameObjects[iSlot][i] = objData; //打開啟用物件用把他update到data裡面去
				break;
			}
		}
		return go;
	}
	
	public bool UnLoadObjectToPool(out int ioutSlotOP, GameObject go)
	{
		//if(iSlot < 0 || iSlot >= m_iNumGameObjectInType) {
		//	return false;	
		//}
		int iSlot = m_GameObjects.Length;
		int iOut = -1;
		bool bRet = false;
		//找出該物件原本的位置，然後關掉他
		for (int i = 0; i < iSlot; i++) {
			if (m_GameObjects [i] != null) {
				int iCount = m_GameObjects [i].Count;
				Debug.Log ("m_GameObjects[iSlot]" + iCount);
				for (int j = 0; j < iCount; j++) {
					ObjectPoolData objData = m_GameObjects [i] [j];
					if (objData.m_go == go) {
						iOut = i;
						ioutSlotOP = iOut;
						objData.m_bUsing = false;
						EnableModel(go, false);
						bRet = true;
						break;
					}
				}
			}
		}
		ioutSlotOP = iOut;
		return bRet;
	}

	public bool FindObjectToPool(out int ioutSlotOP, GameObject go) {
		int iSlot = m_GameObjects.Length;
		int iOut = -1;
		bool bRet = false;
		//if(iSlot < 0 || iSlot >= m_iNumGameObjectInType) {
			//return true;	
		//}
		//Debug.Log ("iCount="+iCount);
		for (int i = 0; i < iSlot; i++) {
			if (m_GameObjects [i] != null) {
				int iCount = m_GameObjects [i].Count;
				Debug.Log ("m_GameObjects[iSlot]" + iCount);
				for (int j = 0; j < iCount; j++) {
					ObjectPoolData objData = m_GameObjects [i] [j];
					if (objData.m_go == go) {
						iOut = i;
						if (objData.m_bUsing == true) {
							ioutSlotOP = iOut;
							bRet = true;
							break;
						}
					}
				}
			}
		}
		ioutSlotOP = iOut;
		return bRet;
	}
	public GameObject FindNowPlayer() {
		GameObject go = null;
		int iSlot = m_GameObjects.Length;
		bool bRet = false;
		//if(iSlot < 0 || iSlot >= m_iNumGameObjectInType) {
		//return true;	
		//}
		//Debug.Log ("iCount="+iCount);
		for (int i = 0; i < iSlot; i++) {
			if (m_GameObjects [i] != null) {
				int iCount = m_GameObjects [i].Count;
				for (int j = 0; j < iCount; j++) {
					ObjectPoolData objData = m_GameObjects [i] [j];
					if(objData.m_go.gameObject.tag == "Player"){
						if (objData.m_bUsing == true) {
							go = objData.m_go;
						}
					}
				}
			}
		}
		return go;
	}
	//針對某一個Slot，把裡面的物件清掉
	public void DestroyPoolSlot(int iSlot)
	{
		if(iSlot < 0 || iSlot >= m_iNumGameObjectInType) {
			return;	
		}
		int iCount = m_GameObjects[iSlot].Count;
		for(int i = 0; i < iCount; i++) {
			ObjectPoolData objData = m_GameObjects[iSlot][i];
			Destroy(objData.m_go);
			m_GameObjects[iSlot][i] = null;
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
		int iLen = aRenders.Length;
		int i;
		for(i = 0; i < iLen; i++) {
			aRenders[i].enabled = bShow;	
		}
	}
	
}