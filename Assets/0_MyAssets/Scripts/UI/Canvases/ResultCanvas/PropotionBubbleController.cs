using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UnityEngine.UI;
using System;
public class PropotionBubbleController : MonoBehaviour
{
    [SerializeField] Text propotionText;
    int count;
    Sequence sequence;
    public void OnInstansiate()
    {
        gameObject.SetActive(false);
        this.ObserveEveryValueChanged(count => this.count)
            .Subscribe(count => ShowPropotion(count))
            .AddTo(this.gameObject);
    }

    public void ShowPropotion(ResultData resultData, Action OnComplete)
    {
        gameObject.SetActive(true);
        var rectTransform = GetComponent<RectTransform>();
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, resultData.worldPos);
        rectTransform.position = screenPos;

        int propotionInt = Mathf.RoundToInt(resultData.proportion);
        count = 0;
        transform.localScale = Vector3.zero;
        sequence = DOTween.Sequence()
            .Append(transform.DOScale(Vector3.one / 2, 0.5f).SetEase(Ease.OutBack))
            .Append(transform.DOScale(Vector3.one, 1f).SetEase(Ease.Linear))
            .Join(DOTween.To(() => count, (x) => count = x, propotionInt, 1f).SetEase(Ease.Linear))
            .AppendInterval(1f)
            .OnComplete(() => OnComplete());
    }

    void ShowPropotion(int propotionInt)
    {
        propotionText.text = propotionInt + " %";
        gameObject.SetActive(true);
    }

    public void OnInitialize()
    {
        sequence.Kill();
        gameObject.SetActive(false);
    }
}
