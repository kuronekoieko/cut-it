using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MeshCutter;
public class CutAnim : MonoBehaviour
{
    void Start()
    {

    }

    public void Anim(CutterTarget originCutterTarget, CutterTarget cutterTarget)
    {
        Vector3 vector = cutterTarget.Renderer.bounds.center - originCutterTarget.Renderer.bounds.center;
        cutterTarget.transform.position += vector.normalized * 0.01f;
        float power = 10f;
        Vector3 force = vector.normalized * power;
        force.x *= 1f;
        force.y *= 2f;
        cutterTarget.Rigidbody.AddForce(force, ForceMode.Acceleration);
    }
}
