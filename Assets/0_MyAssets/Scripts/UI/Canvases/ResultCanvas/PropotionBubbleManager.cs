using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class PropotionBubbleManager : MonoBehaviour
{
    [SerializeField] PropotionBubbleController propotionBubblePrefab;
    List<PropotionBubbleController> propotionBubbleControllers;
    public void OnStart()
    {
        propotionBubbleControllers = new List<PropotionBubbleController>();
        for (int i = 0; i < 2; i++)
        {
            GeneratePropotionBubble(i);
        }
    }

    public void ShowResult()
    {
        //足りない分追加
        for (int i = propotionBubbleControllers.Count; i < Variables.resultDatas.Length; i++)
        {
            GeneratePropotionBubble(i);
        }


        for (int i = 0; i < Variables.resultDatas.Length; i++)
        {
            Action OnComplete = () => { };
            if (i == Variables.resultDatas.Length - 1)
            {
                OnComplete = ClearCheck;
            }
            propotionBubbleControllers[i].ShowPropotion(
                resultData: Variables.resultDatas[i],
                OnComplete: OnComplete);
        }
    }

    void GeneratePropotionBubble(int index)
    {
        propotionBubbleControllers.Add(Instantiate(propotionBubblePrefab, Vector3.zero, Quaternion.identity, transform));
        propotionBubbleControllers[index].OnInstansiate();
    }


    void ClearCheck()
    {
        float average = Variables.resultDatas.Average(r => r.proportion);
        //+-5%以下ならクリア
        bool isInClearRangeAll = Variables.resultDatas.All(r => Mathf.Abs(average - r.proportion) < GameSettingSO.i.errorTolerance);
        Variables.screenState = isInClearRangeAll ? ScreenState.Clear : ScreenState.Failed;
    }

    public void OnInitialize()
    {
        for (int i = 0; i < propotionBubbleControllers.Count; i++)
        {
            propotionBubbleControllers[i].OnInitialize();
        }
    }

}
