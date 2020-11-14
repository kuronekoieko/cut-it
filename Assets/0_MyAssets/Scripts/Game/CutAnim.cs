using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MeshCutter;

public class CutAnim : MonoBehaviour
{
    [SerializeField] int adPattern;
    [SerializeField] Transform leftDishTf;
    [SerializeField] Transform rightDishTf;
    public float Duration
    {
        get
        {
            float duration = 0;
            switch (adPattern)
            {
                case 0: duration = 0.5f; break;
                case 1: duration = 2f; break;
                default: break;
            }
            return duration;
        }
    }

    void Start()
    {

    }

    public void Anim(CutterTarget originCutterTarget, CutterTarget cutterTarget)
    {
        Vector3 vector = cutterTarget.Renderer.bounds.center - originCutterTarget.Renderer.bounds.center;
        switch (adPattern)
        {
            case 0:
                cutterTarget.transform.position += vector.normalized * 0.01f;
                float power = 10f;
                Vector3 force = vector.normalized * power;
                force.x *= 1f;
                force.y *= 2f;
                cutterTarget.Rigidbody.AddForce(force, ForceMode.Acceleration);
                break;
            case 1:
                cutterTarget.Rigidbody.isKinematic = true;
                bool isRight = vector.x > 0;
                Vector3 target = isRight ? rightDishTf.position : leftDishTf.position;
                target.y += cutterTarget.Renderer.bounds.size.y / 2f;
                Vector3[] path = new Vector3[2];
                path[0] = (cutterTarget.transform.position + target) / 2f;
                path[0].y += Mathf.Abs(path[0].y) / 2f;
                path[1] = target;
                cutterTarget.transform.DOPath(path, Duration).SetEase(Ease.OutBounce);
                break;
            default:
                break;
        }
    }
}
