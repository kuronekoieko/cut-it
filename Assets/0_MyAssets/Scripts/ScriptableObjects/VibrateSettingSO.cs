using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/Create VibrateSettingSO", fileName = "VibrateSettingSO")]
public class VibrateSettingSO : ScriptableObject
{
    public SystemSound[] systemSounds;

    private static VibrateSettingSO _i;
    public static VibrateSettingSO i
    {
        get
        {
            string PATH = "ScriptableObjects/" + nameof(VibrateSettingSO);
            //初アクセス時にロードする
            if (_i == null)
            {
                _i = Resources.Load<VibrateSettingSO>(PATH);

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

[System.Serializable]
public class SystemSound
{
    public int systemSoundID;
    public string name;
}