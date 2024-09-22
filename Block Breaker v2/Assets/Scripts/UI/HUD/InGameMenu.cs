using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _inGamemenu;

    public static InGameMenu Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = null;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        _inGamemenu.SetActive(false);
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
}
