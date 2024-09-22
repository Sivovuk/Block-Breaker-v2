using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoadLevels : MonoBehaviour
{
    [SerializeField] private GameObject _levelPrefab;
    [SerializeField] private Transform _levelsParent;
    
    private void OnEnable()
    {
        LoadLevels();
    }

    private void LoadLevels()
    {
        for (int i = 0; i < GameManager.Instance.GameData.boards.Count; i++)
        {
            GameObject spawn = Instantiate(_levelPrefab, _levelsParent);
            spawn.GetComponent<UILevelSetup>().Setup(i+1);
            //Debug.Log(i+1);
        }
    }

    private void OnDisable()
    {
        for (int i = _levelsParent.childCount - 1; i >= 0; i--)
        {
            Destroy(_levelsParent.GetChild(i).gameObject);
        }
    }
}
