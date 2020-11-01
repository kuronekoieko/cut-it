using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCanvasManager : BaseCanvasManager
{
    PropotionBubbleManager propotionBubbleManager;
    public override void OnStart()
    {
        base.SetScreenAction(thisScreen: ScreenState.Result);
        propotionBubbleManager = GetComponent<PropotionBubbleManager>();
        propotionBubbleManager.OnStart();
    }

    public override void OnUpdate()
    {
        if (!base.IsThisScreen) { return; }

    }

    protected override void OnOpen()
    {
        gameObject.SetActive(true);
        propotionBubbleManager.ShowResult();
    }

    protected override void OnClose()
    {
    }

    public override void OnInitialize()
    {
        Close();
        propotionBubbleManager.OnInitialize();
    }


}
