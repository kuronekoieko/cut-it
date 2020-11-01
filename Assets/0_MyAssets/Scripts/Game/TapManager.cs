using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TapType
{
    None,
    Space,
    Stand,
    UI,
}

public class TapManager : MonoBehaviour
{
    public static TapManager i;
    public TapType DownedTapType { get; private set; }

    void Awake()
    {
        i = this;
    }

    public bool GetMouseButtonDown(out TapType tapType)
    {
        tapType = TapType.None;
        if (!Input.GetMouseButtonDown(0)) return false;
        DownedTapType = TapType.UI;
        tapType = TapType.UI;
        if (IsTapUI()) return true;
        DownedTapType = TapType.Stand;
        tapType = TapType.Stand;
        if (IsTapStand()) return true;
        DownedTapType = TapType.Space;
        tapType = TapType.Space;
        return true;
    }

    public bool GetMouseButton(out TapType tapType)
    {
        tapType = TapType.None;
        if (!Input.GetMouseButton(0)) return false;
        tapType = TapType.UI;
        if (IsTapUI()) return true;
        tapType = TapType.Stand;
        if (IsTapStand()) return true;
        tapType = TapType.Space;
        return true;
    }

    public bool GetMouseButtonUp(out TapType tapType)
    {
        tapType = TapType.None;
        if (!Input.GetMouseButtonUp(0)) return false;
        tapType = TapType.UI;
        if (IsTapUI()) return true;
        tapType = TapType.Stand;
        if (IsTapStand()) return true;
        tapType = TapType.Space;
        return true;
    }


    /// <summary>
    /// 【Unity】ボタンを押したときに画面クリックは無視する
    /// https://nn-hokuson.hatenablog.com/entry/2017/07/12/220302
    /// </summary>
    /// <returns></returns>
    bool IsTapUI()
    {
#if UNITY_EDITOR
        return EventSystem.current.IsPointerOverGameObject();
#else
        return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
#endif
    }

    bool IsTapStand()
    {
        var obj = GetTappedObj();
        if (obj == null) return false;
        return obj.CompareTag("Stand");
    }

    GameObject GetTappedObj()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity);
        if (hit.collider == null) return null;
        return hit.collider.gameObject;
    }
}
