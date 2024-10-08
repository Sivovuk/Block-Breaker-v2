﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Level
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _bricksVariante = new List<GameObject>();

        [SerializeField] private Transform _spawnPoint1;
        [SerializeField] private float _spaceBetweenBricksX = 3.25f;
        [SerializeField] private float _spaceBetweenBricksY = 1.25f;

        private void Start()
        {
            LoadLevelData();
        }

        void LoadLevelData()
        {
            float x = _spawnPoint1.position.x;
            float y = _spawnPoint1.position.y;
            
            var board = GameManager.Instance.GameData.boards[GameManager.Instance.LevelIndex-1];

            // Iterate through each row in the board
            for (int rowIndex = 0; rowIndex < board.board.Count; rowIndex++)
            {
                var row = board.board[rowIndex];

                // Iterate through each cell in the row
                for (int colIndex = 0; colIndex < row.row.Count; colIndex++)
                {
                    if (row.row[colIndex] > 0)
                    {
                        //Debug.Log(colIndex);
                        GameObject cell = Instantiate(_bricksVariante[row.row[colIndex]-1], transform);
                        cell.transform.position = new Vector2(x,y);
                    }

                    x += _spaceBetweenBricksX;
                }
                x = _spawnPoint1.position.x;
                y -= _spaceBetweenBricksY;
            }
        }
    }
}