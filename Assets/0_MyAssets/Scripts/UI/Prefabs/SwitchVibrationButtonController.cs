using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
public class SwitchVibrationButtonController : MonoBehaviour
{
    [SerializeField] Sprite onSprite;
    [SerializeField] Sprite offSprite;
    Button vibrationButton;
    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
        vibrationButton = GetComponent<Button>();
    }

    void Start()
    {
        vibrationButton.onClick.AddListener(OnClickVibrationButton);

        //特定の画面で変わったとき、他の画面にも反映させるため
        this.ObserveEveryValueChanged(isOffVibration => SaveData.i.isOffVibration)
            .Subscribe(isOffVibration => { SetSprite(); })
            .AddTo(this.gameObject);
    }

    void OnClickVibrationButton()
    {
        SaveData.i.isOffVibration = !SaveData.i.isOffVibration;
        SaveDataManager.i.Save();
    }

    void SetSprite()
    {
        image.sprite = SaveData.i.isOffVibration ? offSprite : onSprite;
    }


}
