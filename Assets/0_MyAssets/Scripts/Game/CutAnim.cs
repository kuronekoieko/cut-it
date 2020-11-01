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
        return;
        Vector3 vector = cutterTarget.Renderer.bounds.center - originCutterTarget.Renderer.bounds.center;
        cutterTarget.transform.position += vector.normalized * 0.01f;
        Vector3 force = vector.normalized;
        force.x *= 100f;
        force.y *= 200f;
        //cutterTarget.Rigidbody.AddForce(force, ForceMode.Acceleration);
    }
}
