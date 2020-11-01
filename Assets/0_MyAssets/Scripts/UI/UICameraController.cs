using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraController : MonoBehaviour
{
    [SerializeField] ParticleSystem confettiL;
    [SerializeField] ParticleSystem confettiR;

    void Start()
    {

    }

    void Update()
    {
    }

    public void PlayConfetti()
    {
        ShowConfetti(show: true);
        confettiL.Play();
        confettiR.Play();
    }

    public void ShowConfetti(bool show)
    {
        show = Variables.hideConfetti ? false : show;
        confettiL.gameObject.SetActive(show);
        confettiR.gameObject.SetActive(show);
    }
}