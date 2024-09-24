using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private bool _turnOffDetection;

    [SerializeField] private LevelController _levelController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && !_turnOffDetection)
        {
            Destroy(other.gameObject);
        }
    }
}