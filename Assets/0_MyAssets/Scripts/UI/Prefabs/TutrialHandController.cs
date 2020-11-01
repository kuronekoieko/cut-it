using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
using UniRx;
using System;

public class TutrialHandController : MonoBehaviour
{
    [SerializeField] Image handImage;
    Sequence sequence;
    public RectTransform rectTransform { get; private set; }

    public void OnStart()
    {
        rectTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    public void TapAnim(Vector3 startPos)
    {
        Kill();
        Init(startPos);
        sequence = DOTween.Sequence()
            .Append(handImage.rectTransform.DOScale(Vector3.one * 0.8f, 0.5f))
            .Append(handImage.rectTransform.DOScale(Vector3.one, 0.5f));
        sequence.SetLoops(-1);
    }

    public void DragAnim(Vector3[] path, Ease ease, float duration, int loops = -1, Action OnCompleteLoopes = null)
    {
        Kill();
        Init(path[0]);
        sequence = DOTween.Sequence()
            .Append(handImage.rectTransform.DOScale(Vector3.one * 0.8f, 0.5f))
            .Append(rectTransform.DOLocalPath(path, duration).SetEase(ease))
            .Append(handImage.rectTransform.DOScale(Vector3.one, 0.5f))
            .Join(DOTween.ToAlpha(() => handImage.color, color => handImage.color = color, 0f, 0.5f));
        sequence
            .SetLoops(loops)
            .OnComplete(() => OnCompleteLoopes());
    }

    public void Kill()
    {
        gameObject.SetActive(false);
        if (sequence == null) { return; }
        sequence.Kill();
    }

    void Init(Vector3 startPos)
    {
        rectTransform.anchoredPosition = startPos;
        handImage.color = Color.white;
        handImage.rectTransform.localScale = Vector3.one;
        gameObject.SetActive(true);
    }
}