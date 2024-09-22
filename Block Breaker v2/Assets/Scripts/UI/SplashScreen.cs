using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    private void Start()
    {
        transform.DORotate(new Vector3(0,0,360), 1, RotateMode.FastBeyond360).OnComplete(OnRotationComplete);
    }

    private void OnRotationComplete()
    {
        SceneManager.LoadScene(GameManager.MAIN_MENI_SCENE);
    }
}
