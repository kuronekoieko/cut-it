using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshCutter;
using DG.Tweening;

public class PlayerCutter : MonoBehaviour
{
    Collider cutCollider;
    Cutter cutter;
    private Vector3 startPos;
    private Vector3 endPos;
    bool succeededCut;
    Tween cutterMoveTween;
    Tween proportionCheckTween;
    Vector3 startPosObj;
    void Awake()
    {
        cutter = GetComponent<Cutter>();
        cutCollider = GetComponent<Collider>();
    }

    void Start()
    {
        cutCollider.enabled = false;
        startPosObj = transform.position;
    }

    void Update()
    {
        transform.forward = startPosObj - Camera.main.transform.position;
        TapType tapType;
        if (Variables.screenState != ScreenState.Game) { return; }
        if (succeededCut) { return; }
        switch (GameManager.i.GameState)
        {
            case GameState.Waiting:
                if (TapManager.i.GetMouseButtonDown(out tapType) && tapType == TapType.Space)
                {
                    startPos = WorldPosOnTap();
                }
                if (TapManager.i.GetMouseButton(out tapType) && TapManager.i.DownedTapType == TapType.Space)
                {
                    endPos = WorldPosOnTap();
                    if (Vector3.Distance(startPos, endPos) < 0.1f) break;
                    GameManager.i.GameState = GameState.Aiming;
                }
                break;
            case GameState.Aiming:
                if (TapManager.i.GetMouseButtonUp(out tapType))
                {
                    endPos = WorldPosOnTap();
                    MoveCutter();
                    GameManager.i.GameState = GameState.ScissorMoving;
                }
                break;
            case GameState.ScissorMoving:
                break;
            case GameState.ScissorStop:
                startPos = Vector3.zero;
                endPos = Vector3.zero;
                break;
            default:
                break;
        }
    }

    Vector3 WorldPosOnTap()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = Vector3.Distance(startPosObj, Camera.main.transform.position);
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    void MoveCutter()
    {
        cutCollider.enabled = true;
        transform.position = startPos;
        cutterMoveTween = transform.DOMove(endPos, 0.3f).SetEase(Ease.Linear).OnComplete(() =>
        {
            cutCollider.enabled = false;
            if (succeededCut) proportionCheckTween = DOVirtual.DelayedCall(0.5f, () => GameManager.i.ProportionCheck());
            GameManager.i.GameState = GameState.ScissorStop;
        });
    }

    void OnTriggerExit(Collider other)
    {
        var cutterTarget = other.gameObject.GetComponent<CutterTarget>();
        if (cutterTarget == null) { return; }
        var cutPlane = new Plane(Vector3.Cross(transform.forward.normalized, startPos - endPos).normalized, transform.position);
        cutter.Cut(cutterTarget, other.ClosestPointOnBounds(transform.position), cutPlane.normal);
        succeededCut = true;
    }

    void OnDestroy()
    {
        // リトライ時に画面遷移しないようにするため
        cutterMoveTween.Kill();
        proportionCheckTween.Kill();
    }


}
