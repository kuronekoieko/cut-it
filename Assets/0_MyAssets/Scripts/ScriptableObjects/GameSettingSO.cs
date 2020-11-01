using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/Create GameSettingSO", fileName = "GameSettingSO")]
public class GameSettingSO : ScriptableObject
{
    [Header("クリア判定時の誤差の許容範囲(%)")]
    public float errorTolerance;

    private static GameSettingSO _i;
    public static GameSettingSO i
    {
        get
        {
            string PATH = "ScriptableObjects/" + nameof(GameSettingSO);
            //初アクセス時にロードする
            if (_i == null)
            {
                _i = Resources.Load<GameSettingSO>(PATH);

                //ロード出来なかった場合はエラーログを表示
                if (_i == null)
                {
                    Debug.LogError(PATH + " not found");
                }
            }

            return _i;
        }
    }
}