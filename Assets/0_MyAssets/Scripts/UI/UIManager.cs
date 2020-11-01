using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

/// <summary>
/// 画面UIの一括管理
/// GameDirectorと各画面を中継する役割
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _gameDirector;
    public static UIManager i;
    BaseCanvasManager[] canvases;
    static GameObject gameDirector;

    //Awakeより先に呼ばれる
    //シーンをリロードしても呼ばれない
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void RuntimeInitializeApplication()
    {
        //SceneManager.LoadScene("UIScene");
    }

    void Awake()
    {
        i = this;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        if (gameDirector == null)
        {
            DontDestroyOnLoad(_gameDirector);
            gameDirector = _gameDirector;
        }

    }

    void Start()
    {
        i = this;
        SetCanvases();
        Variables.currentSceneBuildIndex++;
        SceneManager.LoadScene(Variables.currentSceneBuildIndex);
        // イベントにイベントハンドラーを追加
        SceneManager.sceneLoaded += SceneLoaded;
    }

    void SetCanvases()
    {
        canvases = new BaseCanvasManager[transform.childCount];
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i] = transform.GetChild(i).GetComponent<BaseCanvasManager>();
            if (canvases[i] == null) { continue; }
            canvases[i].OnStart();
        }
    }

    void Update()
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            if (canvases[i] == null) { continue; }
            canvases[i].OnUpdate();
        }

        if (Variables.screenState == ScreenState.Initialize)
        {
            InitiaLize();
        }
    }

    void InitiaLize()
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            if (canvases[i] == null) { continue; }
            canvases[i].OnInitialize();
        }
        //1フレーム後に実行する
        //同一フレーム内で変数が変更されると、UniRxが反応しないため
        //【Unity】スクリプトの処理の実行タイミングを操作する
        // https://qiita.com/toRisouP/items/e402b15b36a8f9097ee9
        Observable.TimerFrame(1)
            .Subscribe(_ => Variables.screenState = ScreenState.Game);
    }


    // イベントハンドラー（イベント発生時に動かしたい処理）
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        Variables.screenState = ScreenState.Initialize;
    }
}