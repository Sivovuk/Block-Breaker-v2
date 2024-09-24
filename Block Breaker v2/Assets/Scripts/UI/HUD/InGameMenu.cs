using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _inGamemenu;
    [SerializeField] private TMP_Text _scoreTMP;
    [SerializeField] private TMP_Text _lifes;

    [Header("Game Won UI")] 
    [SerializeField] private GameObject _uiGameResult;
    [SerializeField] private TMP_Text _uiResultHeaderTMP;
    [SerializeField] private TMP_Text _uiResultScoreTMP;
    [SerializeField] private Button _uiResultBtn;
    
    public static InGameMenu Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = null;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        _inGamemenu.SetActive(true);
        PaddleController.Instance.GameStop(true);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        _inGamemenu.SetActive(false);
        PaddleController.Instance.GameStop(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Back()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(GameManager.MAIN_MENI_SCENE);
    }

    public void GameWon(int scoreValue)
    {
        _uiGameResult.gameObject.SetActive(true);
        Time.timeScale = 0;
        _uiResultHeaderTMP.text = "Level Passed";
        _uiResultHeaderTMP.color = Color.green;
        _uiResultScoreTMP.text = "Score : " + scoreValue.ToString();
        _uiResultBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Next";
        _uiResultBtn.onClick.AddListener(NextLevel);
    }
    
    public void GameLost(int scoreValue)
    {
        _uiGameResult.gameObject.SetActive(true);
        Time.timeScale = 0;
        _uiResultHeaderTMP.text = "Level Failed";
        _uiResultHeaderTMP.color = Color.red;
        _uiResultScoreTMP.text = "Score : " + scoreValue.ToString();
        _uiResultBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Restart";
        _uiResultBtn.onClick.AddListener(Restart);
    }

    public void UpdateScoreTMP(int scoreValue)
    {
        _uiResultScoreTMP.text = "Score : " + scoreValue.ToString();
    }
    
    public void UpdateLifesTMP(int lifesValue)
    {
        _lifes.text = "Lifes : " + lifesValue.ToString();
    }

    private void NextLevel()
    {
        bool check = GameManager.Instance.SetLevelIndex(GameManager.Instance.LevelIndex+1);
        
        if (check)
            Restart();
        else
            Back();
    }

}
