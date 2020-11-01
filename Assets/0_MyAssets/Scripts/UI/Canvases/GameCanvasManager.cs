using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// ゲーム画面
/// ゲーム中に表示するUIです
/// あくまで例として実装してあります
/// ボタンなどは適宜編集してください
/// </summary>
public class GameCanvasManager : BaseCanvasManager
{
    [SerializeField] Text stageNumText;
    [SerializeField] TutrialHandController tutrialHandController;
    [SerializeField] Button retryButton;
    [SerializeField] RectTransform startPointRTf;
    [SerializeField] RectTransform endPointRTf;
    [SerializeField] RectTransform DotLineRTf;
    [SerializeField] RectTransform scissorRTf;
    PlayerCutter playerCutter;
    public static GameCanvasManager i;

    public override void OnStart()
    {
        i = this;

        base.SetScreenAction(thisScreen: ScreenState.Game);

        this.ObserveEveryValueChanged(currentStageIndex => Variables.currentSceneBuildIndex)
            .Subscribe(currentStageIndex => { ShowStageNumText(); })
            .AddTo(this.gameObject);

        gameObject.SetActive(true);
        tutrialHandController.OnStart();
        retryButton.onClick.AddListener(OnClickRetryButton);
    }

    public override void OnUpdate()
    {
        if (!base.IsThisScreen) { return; }
        if (GameManager.i == null) { return; }
        switch (GameManager.i.GameState)
        {
            case GameState.Waiting:
                if (Input.GetMouseButtonDown(0))
                {
                    startPointRTf.position = Input.mousePosition;
                    scissorRTf.anchoredPosition = Vector3.zero;
                }
                break;
            case GameState.Aiming:
                startPointRTf.gameObject.SetActive(true);
                endPointRTf.gameObject.SetActive(true);
                endPointRTf.position = Input.mousePosition;
                startPointRTf.up = startPointRTf.position - Input.mousePosition;
                var sizeDelta = DotLineRTf.sizeDelta;
                sizeDelta.x = Vector3.Distance(startPointRTf.anchoredPosition, endPointRTf.anchoredPosition);
                DotLineRTf.sizeDelta = sizeDelta;
                break;
            case GameState.ScissorMoving:
                Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, playerCutter.transform.position);
                scissorRTf.position = screenPos;
                break;
            case GameState.ScissorStop:
                startPointRTf.gameObject.SetActive(false);
                endPointRTf.gameObject.SetActive(false);
                GameManager.i.GameState = GameState.Waiting;
                break;
            default:
                break;
        }
    }

    protected override void OnOpen()
    {
        gameObject.SetActive(!Variables.hideGameCanvas);
        startPointRTf.gameObject.SetActive(false);
        endPointRTf.gameObject.SetActive(false);
    }

    protected override void OnClose()
    {
        // gameObject.SetActive(false);
    }

    public override void OnInitialize()
    {
        playerCutter = FindObjectOfType<PlayerCutter>();
    }

    void ShowStageNumText()
    {
        stageNumText.text = "LEVEL " + (Variables.currentSceneBuildIndex).ToString("000");
    }

    void OnClickRetryButton()
    {
        bool canClick = Variables.screenState == ScreenState.Game || Variables.screenState == ScreenState.Result;
        if (!canClick) { return; }
        //AdManager.i.Interstitial.ShowInterstitial();
        base.ReLoadScene();
    }
}
