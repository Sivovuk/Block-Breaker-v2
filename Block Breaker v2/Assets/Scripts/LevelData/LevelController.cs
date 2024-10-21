using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Bricks;
using Gameplay.Player;
using Gameplay.Power_ups;
using Level;
using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{
    [SerializeField] private List<PowerupDrop> _powerups = new List<PowerupDrop>();

    [SerializeField] private InGameMenu _inGameMenu;
    [SerializeField] private LevelLoader _levelLoader;
    
    [SerializeField] private List<int> _powerupsIndexes;
    [SerializeField] private List<float> _powerupsDropPercentage;
    [SerializeField] private List<Row> _level;
    [SerializeField] private float _percentageReduction = 10f;

    [SerializeField] private int _currentLevelIndex;

    private int _score = 0;
    public Action<int> OnScoreChange;
    [SerializeField] private int _lifes = 3;
    public Action<int> OnLifeChange;
    private List<GameObject> _activeBalls = new List<GameObject>();

    [SerializeField] private float _dropsCooldown = 5f;
    private float _timePassed;
    private bool _canDrop;

    public static LevelController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        SetupLevelController();
        OnScoreChange += _inGameMenu.UpdateScoreTMP;
        OnLifeChange += _inGameMenu.UpdateLifesTMP;
    }

    private void Update()
    {
        if (_canDrop) return;
        
        _timePassed += Time.deltaTime;
        if (_timePassed >= _dropsCooldown)
        {
            _timePassed = 0;
            _canDrop = true;
        }
    }

    private void OnDestroy()
    {
        OnScoreChange -= _inGameMenu.UpdateScoreTMP;
        OnLifeChange -= _inGameMenu.UpdateLifesTMP;
    }

    private void SetupLevelController()
    {
        
        _currentLevelIndex = GameManager.Instance.LevelIndex;
        _powerupsIndexes = GameManager.Instance.GameData.boards[_currentLevelIndex-1].allowedPowerUps;
        _level = GameManager.Instance.GameData.boards[_currentLevelIndex-1].board;

        if (_powerupsIndexes.Count <= 0)return;
        
        float initialPercentage = 100 / _powerupsIndexes.Count;

        foreach (var powerup in _powerupsIndexes)
            _powerupsDropPercentage.Add(initialPercentage);
    }

    public void BrickDestroy(Vector3 position, int rewardValue)
    {
        SpawnPowerup(position);
        _score += rewardValue;
        OnScoreChange.Invoke(_score);

    }

    public void UpdateBricks(List<BasicBrick> bricks)
    {
        int stoneBricks = bricks.Count(x => x.Type == BricksType.Stone);
        if (bricks.Count - stoneBricks <= 0)
            GameWon();
    }

    private void GameWon()
    {
        _inGameMenu.GameWon(_score);
        PaddleController.Instance.GameStop(true);
    }

    public void LifeDecrese()
    {
        _lifes--;
        OnLifeChange.Invoke(_lifes);

        if (_lifes <= 0)
            GameLost();

        PaddleController.Instance.AddNewBall();
    }

    private void GameLost()
    {
        PaddleController.Instance.GameStop(true);
        _inGameMenu.GameLost(_score);
    }

    #region Powerups

    private void SpawnPowerup(Vector3 position)
    {
        if (!_canDrop)return;
        
        int index = SelectPowerup();
        if (index == -1) return;
        
        GameObject spawn = Instantiate(_powerups[index].gameObject, position, Quaternion.identity);
        Destroy(spawn, 5f);
        
        _canDrop = false;
    }

    private int SelectPowerup()
    {
        if (_powerupsDropPercentage.Count <= 0) return -1;
        
        int index = _powerupsDropPercentage.IndexOf(_powerupsDropPercentage.Max());
        DropPowerupPercentage(index);
        return _powerupsIndexes[index]-1;
    }

    void DropPowerupPercentage(int droppedIndex)
    {
        float dropValue = _powerupsDropPercentage[droppedIndex] * (_percentageReduction / 100f);
        _powerupsDropPercentage[droppedIndex] -= dropValue; // Decrease the percentage for the dropped object

        // Distribute 90% of the dropValue to other objects
        float redistributionValue = dropValue * 0.9f;
        float redistributionPerObject = redistributionValue / (_powerupsIndexes.Count - 1);

        for (int i = 0; i < _powerupsDropPercentage.Count; i++)
        {
            if (i != droppedIndex)
            {
                _powerupsDropPercentage[i] += redistributionPerObject; // Increase percentage for other objects
            }
        }

        // Ensure percentages don't exceed 100% (optional normalization)
        NormalizePercentages();

        // Debug: Print the new drop percentages
        //Debug.Log("New Drop Percentages: " + string.Join(", ", _powerupsDropPercentage));
    }

    void NormalizePercentages()
    {
        float total = 0f;
        foreach (float percentage in _powerupsDropPercentage)
        {
            total += percentage;
        }

        // Normalize percentages to ensure they sum up to 100%
        for (int i = 0; i < _powerupsDropPercentage.Count; i++)
        {
            _powerupsDropPercentage[i] = (_powerupsDropPercentage[i] / total) * 100f;
        }
    }

    public void PowerupPickedUp(int rewardValue)
    {
        _score += rewardValue;
        OnScoreChange.Invoke(_score);
    }

    #endregion

    public void MultyBalls(float duration)
    {
        StartCoroutine(MultyBallsCorutine(duration));
    }

    public IEnumerator MultyBallsCorutine(float duration)
    {
        yield return new WaitForSeconds(duration);

        List<BallController> balls = FindObjectsOfType<BallController>().ToList();

        foreach (var ball in balls)
        {
            if (ball == null) continue;

            if (BallController._ballCounter > 1)
            {
                Destroy(ball);
            }
            
            else if (BallController._ballCounter == 1)
            {
                PaddleController.Instance.SetBallController(ball.GetComponent<BallController>());
                break;
            }
        }
    }
}
