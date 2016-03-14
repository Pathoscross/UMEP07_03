using System.Collections;
using UnityEditor;
using UnityEngine;

public class SmoothDamp : MonoBehaviour
{
    public Transform target; //目標
    private Vector3 targetPosition;
    private Vector3 targetPosition_temp;

    public bool Obs = false;

    public float Maxdistance = -5; //攝影機與主角的 MAX距離
    public float Mindistance = -0.55f; //攝影機與主角的 MIN距離

    public bool CameraFollowing = true;
    public bool Smooth = true;

    public float smoothTime = 0.3F; //Smooth時間
    public float mouseWheelSpeed = 1.0f;
    private Vector3 velocity = Vector3.zero;

    //Camera設定座標

    public float TransformPosX = 0;
    public float TransformPosY = 1.5f;
    public float TransformPosZ = -2;
    //*************

    //Camera設定Rotaion

    public float MaxAxixX = 45;
    public float MinAxixX = -45;
    public float MaxAxixY = 180;
    public float MinAxixY = -180;
    public float MouseX;
    public float MouseY;
    private Quaternion CameraRotationEuler;
    private Quaternion CharRotationEuler;

    //LookAt目標修正
    public Vector3 targetPos = Vector3.zero;

    //Camera初始座標

    private float _transformPosX
    { get { return TransformPosX; } set { } }

    private float _transformPosY
    { get { return TransformPosY; } set { } }

    private float _transformPosZ
    {
        get { return TransformPosZ; }
        set
        {
            if (value > Mindistance)
            {
                value = Mindistance;
                TransformPosZ = value;
            }
            else if (value < Maxdistance)
            {
                value = Maxdistance;
                TransformPosZ = value;
            }
            else
            {
                TransformPosZ = value;
            }
        }
    }

    //*************

    private void Awake()
    {
        //攝影機位置初始化
        Vector3 targetPosition = target.TransformPoint(new Vector3(_transformPosX, _transformPosY, _transformPosZ));
        transform.position = targetPosition;
        //*************
    }

    private void LateUpdate()
    {
        transform.rotation = CameraRotationEuler;
        target.rotation = CharRotationEuler;

        //攝影機 Z值設定
        CameraControl();
        //攝影機目標跟隨
        transform.LookAt(LookAtTarget());
        //設定攝影機位置
        targetPosition = target.TransformPoint(new Vector3(_transformPosX, _transformPosY, _transformPosZ));
        //鏡頭與玩家之間碰撞判定
        if (DetermineObstacle()) { MoveCameraToObstaclePoint(); }

        //Smooth跟隨方式
        if (Smooth) { SmoothFindingTarget(); }
        else { targetPosition_temp = targetPosition; }
        //攝影機跟隨開關
        if (CameraFollowing) { transform.position = targetPosition_temp; }

        //控制玩家左右旋轉
        target.rotation = CharRotationEuler;
    }

    private void SmoothFindingTarget()
    {
        targetPosition_temp = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    public void CameraControl()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float fovVaule = (Input.GetAxis("Mouse ScrollWheel") * mouseWheelSpeed);

            if (Obs)
            {
                if (fovVaule < 0) { fovVaule = 0; }
            }

            _transformPosZ += fovVaule;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed left click.");
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Pressed right click.");
        }
        if (Input.GetMouseButton(1))
        {
            // CameraFollowing = false;

            TransformPosY += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseWheelSpeed;
            MouseX += Input.GetAxis("Mouse X") * Time.deltaTime * mouseWheelSpeed;

            CharRotationEuler = Quaternion.EulerAngles(0, MouseX, 0);
            //  TransformPosY += MouseY * Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(1))
        {
            // CameraFollowing = true;
            Debug.Log("放開滑鼠");
        }
    }

    public static float CameraClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }

    private Vector3 LookAtTarget()
    {
        Vector3 lookAt = target.position + targetPos;
        return lookAt;
    }

    //判斷攝影機Prob與玩家角色之間是否有障礙物
    private bool DetermineObstacle()
    {
        RaycastHit hitInfo;

        if (Physics.Linecast(transform.position, (LookAtTarget()), out hitInfo))
        {
            Debug.Log("碰撞物名稱：" + hitInfo.transform.name);
            Obs = true;
            return true;
        }

        Obs = false;
        return false;
    }

    //限制攝影機 Z 值不得大於碰撞點
    private void MoveCameraToObstaclePoint()
    {
        Vector3 dirction = transform.position - target.position;
        float Dist = dirction.magnitude;
        dirction.Normalize();

        Ray ray = new Ray(target.position, dirction);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            Vector3 colPoint = hitInfo.point;
            Vector3 tDist = target.position - colPoint;
            float tcDist = tDist.magnitude;
            TransformPosZ = -tcDist;
            targetPosition = colPoint;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, LookAtTarget());
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.transform.forward * 0.1f);
    }
}