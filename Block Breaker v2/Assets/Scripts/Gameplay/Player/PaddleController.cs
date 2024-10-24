using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Power_ups;
using Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.Player
{
    public class PaddleController : MonoBehaviour, ICollectable
    {
        public bool BallLocked { get; private set; } = true;

        private bool _isGunActive;
        private bool _isGameStop;
        [SerializeField] private GameObject _gun;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Bullet _bullet;
        [SerializeField] private BallController _ball;
        [SerializeField] private Transform _ballSpawnPoint;

        public BallController BallController { get; private set; }
        private Camera _mainCamera;
        public static PaddleController Instance { get; private set; }
        [SerializeField] private LevelController _levelController;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            BallController = FindObjectOfType<BallController>();
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            PaddleMove();
            
            if (BallLocked && (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space)) && !EventSystem.current.IsPointerOverGameObject())
            {
                BallLocked = false;
                BallController.ReleaseBall();
            }

            if (_isGunActive && (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.X)) && !EventSystem.current.IsPointerOverGameObject())
            {
                GameObject spawn = Instantiate(_bullet.gameObject, _shootPoint.position, Quaternion.identity);
                Destroy(spawn, 5f);
            }
        }

        private void PaddleMove()
        {
            if (_isGameStop) return;
            
            Vector3 minScreenBounds = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, _mainCamera.nearClipPlane));
            Vector3 maxScreenBounds = _mainCamera.ViewportToWorldPoint(new Vector3(1, 0, _mainCamera.nearClipPlane));
            
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.y = transform.position.y;
            mousePos.x = Mathf.Clamp(mousePos.x, minScreenBounds.x + (transform.localScale.x / 2),
                maxScreenBounds.x - (transform.localScale.x / 2));
            transform.position = mousePos;
        }

        public void GameStop(bool value)
        {
            _isGameStop = value;
        }

        public void PaddleExtend(float duration)
        {
            StartCoroutine(PaddleExtendCorutine(duration));
        }

        private IEnumerator PaddleExtendCorutine(float duration)
        {
            transform.localScale = new Vector2(transform.localScale.x*2, transform.localScale.y);
            yield return new WaitForSeconds(duration);
            transform.localScale = new Vector2(transform.localScale.x/2, transform.localScale.y);
        }

        public void GunActivation(float duration)
        {
            StartCoroutine(GunActivationCorutine(duration));
        }

        public IEnumerator GunActivationCorutine(float duration)
        {
            _isGunActive = true;
            _gun.gameObject.SetActive(true);
            yield return new WaitForSeconds(duration);
            _isGunActive = false;
            _gun.gameObject.SetActive(false);
        }

        public void Collect(CollectableType type, float duration, int rewardValue)
        {
            _levelController.PowerupPickedUp(rewardValue);
            
            if (type == CollectableType.Fireball)
            {
                StartCoroutine(BallController.FireballCorutine(duration));
            }
            else if (type == CollectableType.Bomb)
            {
                BallController.SetBallAsBomb();
            }

        }

        public void SetBallController(BallController ballController)
        {
            BallController = ballController;
        }

        public void AddNewBall()
        {
            if (_isGameStop) return;
            
            GameObject spawn = Instantiate(_ball.gameObject, _ballSpawnPoint.position, Quaternion.identity);
            BallLocked = true;
            BallController = spawn.GetComponent<BallController>();
        }
    }
}