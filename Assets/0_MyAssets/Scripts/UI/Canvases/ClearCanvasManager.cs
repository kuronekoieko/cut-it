using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class ClearCanvasManager : BaseCanvasManager
{
    [SerializeField] Button nextButton;
    [SerializeField] Text coinCountText;
    [SerializeField] Button homeButton;
    [SerializeField] CoinCountView coinCountView;
    [SerializeField] UICameraController uICameraController;

    public override void OnStart()
    {
        base.SetScreenAction(thisScreen: ScreenState.Clear);

        nextButton.onClick.AddListener(OnClickNextButton);
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
        uICameraController.PlayConfetti();
        SoundManager.i.PlayOneShot(0);
        DOVirtual.DelayedCall(1.2f + Variables.screenDelaySec, () =>
        {
            gameObject.SetActive(true);
        });
    }

    protected override void OnClose()
    {
        gameObject.SetActive(false);
        uICameraController.ShowConfetti(show: false);
    }

    public override void OnInitialize()
    {

    }

    void OnClickNextButton()
    {
        base.ToNextScene();
    }

    void OnClickHomeButton()
    {
        Variables.screenState = ScreenState.Home;
    }
}
