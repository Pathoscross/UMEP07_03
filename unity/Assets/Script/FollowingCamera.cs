using System.Collections;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    private Transform m_Target = null;

    private void Start()
    {
    }

    private void Update()
    {
        CameraControl(this.transform.position);
    }

    private void CameraControl(Vector3 CameraPos)
    {
        if (m_Target == null)
        {
            m_Target = ObjectPool.m_Instance.FindNowPlayer().transform;
        }

        float x = m_Target.position.x;
        float y = this.transform.position.y;
        float z = this.transform.position.z;

        if (CameraPos.x != m_Target.transform.position.x)
        {
            this.transform.position = new Vector3(x, y, z);
        }
    }
}