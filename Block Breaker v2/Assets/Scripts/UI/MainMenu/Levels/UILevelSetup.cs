using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevelSetup : MonoBehaviour
{
    private TMP_Text _levelIndexTMP;
    private Button _levelBtn;

    private int _levelIndex;
    
    public void Setup(int index)
    {
        _levelBtn = GetComponent<Button>();
        _levelBtn.onClick.AddListener(LoadLevel);
        _levelIndexTMP = GetComponentInChildren<TMP_Text>();
        _levelIndexTMP.text = index.ToString();
        _levelIndex = index;
    }

    private void LoadLevel()
    {
        GameManager.Instance.SetLevelIndex(_levelIndex);
        SceneManager.LoadScene(GameManager.GAME_SCENE);
    }
}
