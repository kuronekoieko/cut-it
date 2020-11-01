using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;

/// <summary>
/// 画面のスクリプトに継承して使う
/// UniRxでVariables.screenStateを監視し、
/// 画面の開閉処理をやってくれる
/// </summary>
public class BaseCanvasManager : MonoBehaviour
{
    ScreenState thisScreen;
    protected bool IsThisScreen => Variables.screenState == thisScreen;

    /// <summary>
    /// OnOpenとOnCloseのイベント発火タイミングを設定する
    /// </summary>
    /// <param name="thisScreen">セットする画面のenumを入れてください</param>
    protected void SetScreenAction(ScreenState thisScreen)
    {
        this.thisScreen = thisScreen;

        this.ObserveEveryValueChanged(screenState => Variables.screenState)
            .Where(screenState => screenState == thisScreen)
            .Subscribe(screenState => OnOpen())
            .AddTo(this.gameObject);

        this.ObserveEveryValueChanged(screenState => Variables.screenState)
            .Buffer(2, 1)
            .Where(screenState => screenState[0] == thisScreen)
            .Where(screenState => screenState[1] != thisScreen)
            .Subscribe(screenState => OnClose())
            .AddTo(this.gameObject);
    }

    public virtual void OnStart()
    {
    }

    public virtual void OnUpdate()
    {
    }

    /// <summary>
    /// シーンのリロードが完了したタイミングで呼ばれる。
    /// UIの初期化に使用
    /// </summary>
    public virtual void OnInitialize()
    {

    }

    /// <summary>
    /// 画面が開かれる瞬間だけ呼ばれる
    /// </summary>
    protected virtual void OnOpen()
    {
    }

    /// <summary>
    /// 画面が閉じられる瞬間だけ呼ばれる
    /// </summary>
    protected virtual void OnClose()
    {
    }

    /// <summary>
    /// シーンのリロードが完了すると、UIManager.SceneLoadedが呼ばれます。
    /// </summary>
    protected void ToNextScene()
    {
        Variables.currentSceneBuildIndex++;
        SceneManager.LoadScene(Variables.currentSceneBuildIndex);
    }

    /// <summary>
    /// シーンのリロードが完了すると、UIManager.SceneLoadedが呼ばれます。
    /// </summary>
    protected void ReLoadScene()
    {
        SceneManager.LoadScene(Variables.currentSceneBuildIndex);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }



    /* 
        public override void OnStart()
        {
        if (!base.IsThisScreen()) { return; }
        }

        public override void OnUpdate(ScreenState currentScreen)
        {
            if (currentScreen != thisScreen) { return; }

        }

        protected override void OnOpen()
        {
            gameObject.SetActive(true);
        }

        protected override void OnClose()
        {
        }
        public override void OnInitialize()
    {

    }
    */

}
