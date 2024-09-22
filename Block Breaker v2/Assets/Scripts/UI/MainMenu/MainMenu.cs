using System;
using UnityEngine;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public static MainMenu Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = null;
            else
                Destroy(gameObject);
        }

        public void LoadLevel(int levelIndex)
        {
            GameManager.Instance.SetLevelIndex(levelIndex);
        }
    }
}