using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム内で使う変数
/// UIに表示するときはUniRxで値を監視するのがおすすめ
/// ・Unityで学ぶMVPパターン ~ UniRxを使って体力Barを作成する ~
/// https://qiita.com/Nakashima_Hibari/items/5e0c36c3b0df78110d32
/// </summary>
public class Variables : MonoBehaviour
{
    public static ScreenState screenState = ScreenState.Game;
    public static int currentSceneBuildIndex
    {
        set { _currentSceneBuildIndex = Mathf.Clamp(value, 1, SceneManager.sceneCountInBuildSettings - 1); }
        get { return _currentSceneBuildIndex; }
    }
    private static int _currentSceneBuildIndex;
    public static float screenDelaySec;
    public static bool hideGameCanvas;
    public static bool hideConfetti;
    public static ResultData[] resultDatas;
}

public struct ResultData
{
    public float proportion;
    public Vector3 worldPos;
}
