using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshCutter;
public class CameraRotation : MonoBehaviour
{
    Vector3 targetPos;
    void Awake()
    {

    }

    void Start()
    {
        var cutterTarget = FindObjectOfType<CutterTarget>();
        targetPos = cutterTarget.Renderer.bounds.center;
    }

    void Update()
    {
        CamWorkController();
    }



    public void CamWorkController()
    {
        TapType tapType;
        if (TapManager.i.GetMouseButtonDown(out tapType) && tapType == TapType.Stand)
        {
        }
        if (TapManager.i.GetMouseButton(out tapType) && TapManager.i.DownedTapType == TapType.Stand)
        {
            Rotate();
        }
    }

    /// <summary>
    /// Unityでカメラの向きを基準に移動する方法と、追従して回転できるカメラの実装
    /// https://tech.pjin.jp/blog/2016/11/04/unity_skill_5/
    /// </summary>
    void Rotate()
    {
        float mouseInputX = Input.GetAxis("Mouse X");
        float mouseInputY = Input.GetAxis("Mouse Y");
        // targetの位置のY軸を中心に、回転（公転）する
        transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 400f);
        // カメラの垂直移動

        var diffAngleX = mouseInputY * Time.deltaTime * 500f;
        var rotatedAngleX = transform.eulerAngles.x - diffAngleX;
        if (rotatedAngleX < 0 || 70 < rotatedAngleX) { return; }
        transform.RotateAround(targetPos, -transform.right, diffAngleX);
    }
}
