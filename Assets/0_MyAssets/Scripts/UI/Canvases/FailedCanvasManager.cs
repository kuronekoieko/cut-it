using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class FailedCanvasManager : BaseCanvasManager
{
    [SerializeField] Button restartButton;
    [SerializeField] Button homeButton;
    [SerializeField] Text coinCountText;
    [SerializeField] CoinCountView coinCountView;

    public override void OnStart()
    {
        base.SetScreenAction(thisScreen: ScreenState.Failed);
        restartButton.onClick.AddListener(OnClickRestartButton);
        homeButton.onClick.AddListener(OnClickHomeButton);
        gameObject.SetActive(false);
        coinCountView.OnStart();
    }

    public override void OnUpdate()
    {
        if (!base.IsThisScreen) { return; }

    }

    protected override void OnOpen()
    {
        DOVirtual.DelayedCall(0f + Variables.screenDelaySec, () =>
        {
            gameObject.SetActive(true);
        });
    }

    protected override void OnClose()
    {
        gameObject.SetActive(false);
    }

    public override void OnInitialize()
    {

    }

    void OnClickRestartButton()
    {
        base.ReLoadScene();
    }

    void OnClickHomeButton()
    {
        Variables.screenState = ScreenState.Home;
    }
}
