using System;
using System.IO;
using Level;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const string SPLASHSCREEN_SCENE = "SplashScreen";
    public const string MAIN_MENI_SCENE = "MainMenu";
    public const string GAME_SCENE = "Game";
    
    public static GameManager Instance { get; private set; }

    public int LevelIndex { get; private set; }
    public GameData GameData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        LoadLevelData();
    }

    private void LoadLevelData()
    {
        string filePath = Application.dataPath + "/Scripts/LevelData/levels.json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            GameData = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Levels loaded, number of levels : " + GameData.boards.Count);
        }
        else
        {
            Debug.LogError("JSON file not found: " + filePath);
        }
    }

    public void SetLevelIndex(int index)
    {
        if (index > 0)
        {
            LevelIndex = index;
        }
        else
        {
            Debug.LogError("Bad level index, less then 0");
        }
    }
}