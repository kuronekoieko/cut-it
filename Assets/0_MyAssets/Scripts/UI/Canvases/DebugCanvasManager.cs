using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;

public class DebugCanvasManager : BaseCanvasManager
{
    [Header("バナー")]
    [SerializeField] Button clearButton;
    [SerializeField] Button failButton;
    [SerializeField] Button hideBannerButton;
    [SerializeField] Button debugButton;
    [SerializeField] Image bannerImage;
    [SerializeField] Button restartButton;

    [Header("デバッグ画面")]
    [SerializeField] Image debugPanel;
    [SerializeField] Button applyButton;
    [SerializeField] Button cancelButton;
    [SerializeField] Toggle screenDelayToggle;
    [SerializeField] Toggle hideGameCanvasToggle;
    [SerializeField] Toggle hideConfettiToggle;
    [SerializeField] Toggle resetSaveDataToggle;
    [SerializeField] Dropdown stageNumDd;

    public override void OnStart()
    {
        gameObject.SetActive(Debug.isDebugBuild);
        hideBannerButton.onClick.AddListener(OnClickHideBannerButton);
        debugButton.onClick.AddListener(OnClickDebugButton);
        debugPanel.gameObject.SetActive(false);
        applyButton.onClick.AddListener(OnClickApplyButton);
        cancelButton.onClick.AddListener(OnClickCancelButton);
        restartButton.onClick.AddListener(OnClickRestartButton);
        clearButton.onClick.AddListener(OnClickClearButton);
        failButton.onClick.AddListener(OnClickFailButton);

        List<string> numStrings = new List<string>();
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            numStrings.Add((i) + "  " + Path.GetFileName(SceneUtility.GetScenePathByBuildIndex(i)));
        }
        stageNumDd.ClearOptions();
        stageNumDd.AddOptions(numStrings);
    }

    /// <summary>
    /// inputfieldとかtoggleにデータを入れる
    /// </summary>
    void ShowParam()
    {
        screenDelayToggle.isOn = Variables.screenDelaySec != 0;
        hideGameCanvasToggle.isOn = Variables.hideGameCanvas;
        hideConfettiToggle.isOn = Variables.hideConfetti;
        resetSaveDataToggle.isOn = false;
        stageNumDd.value = Variables.currentSceneBuildIndex - 1;
    }

    void OnClickHideBannerButton()
    {
        bannerImage.gameObject.SetActive(!bannerImage.gameObject.activeSelf);
        hideBannerButton.GetComponent<CanvasGroup>().alpha = bannerImage.gameObject.activeSelf ? 1 : 0;
    }

    void OnClickDebugButton()
    {
        debugPanel.gameObject.SetActive(true);
        ShowParam();
    }

    void OnClickApplyButton()
    {
        Variables.screenDelaySec = screenDelayToggle.isOn ? 3 : 0;
        Variables.hideGameCanvas = hideGameCanvasToggle.isOn;
        Variables.hideConfetti = hideConfettiToggle.isOn;
        TransStage();
        SaveDataManager.i.Save();
        //削除後にセーブ処理を入れると、データが復活する
        if (resetSaveDataToggle.isOn) { PlayerPrefs.DeleteAll(); }
        Close();
    }

    void OnClickCancelButton()
    {
        Close();
    }

    void Close()
    {
        debugPanel.gameObject.SetActive(false);
    }

    void TransStage()
    {
        Variables.currentSceneBuildIndex = stageNumDd.value + 1;
        SceneManager.LoadScene(Variables.currentSceneBuildIndex);
    }
    void OnClickRestartButton()
    {
        if (Variables.screenState != ScreenState.Game) { return; }
        base.ReLoadScene();
    }
    void OnClickClearButton()
    {
        if (Variables.screenState != ScreenState.Game) { return; }
        Variables.screenState = ScreenState.Clear;
    }
    void OnClickFailButton()
    {
        if (Variables.screenState != ScreenState.Game) { return; }
        Variables.screenState = ScreenState.Failed;
    }
}
