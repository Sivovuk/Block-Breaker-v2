using System;
using Gameplay.Player;
using UI.HUD;
using UnityEngine;

public enum CollectableType
{
    Multiply = 1,
    PaddleExtend = 2,
    Fireball = 3,
    Bomb = 4,
    Gun = 5
}

namespace Gameplay.Power_ups
{
    public class PowerupDrop : MonoBehaviour
    {
        [SerializeField] protected float _fallSpeed = 10f;
        [SerializeField] protected float _duration;
        [SerializeField] protected int _rewardValue = 10;
        [SerializeField] protected CollectableType _type;

        protected Camera _mainCamera;
        protected Vector3 _minScreenBounds;

        protected virtual void Start()
        {
            _mainCamera = Camera.main;
            _minScreenBounds = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, _mainCamera.nearClipPlane));
        }

        protected virtual void Update()
        {
            transform.Translate(Vector3.down * _fallSpeed * Time.deltaTime);

            if (transform.position.y - 2 < _minScreenBounds.y)
                Destroy(gameObject);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider2D)
        {
            if (collider2D.CompareTag("Paddle") && collider2D.TryGetComponent<PaddleController>(out PaddleController ball))
            {
                PaddleController.Instance.Collect(_type, _duration, _rewardValue);
                Collected(PaddleController.Instance.BallController.gameObject);
            }
        }

        protected virtual void Collected(GameObject ball)
        {
            if (_type != CollectableType.Bomb)
            {
                PowerupsTimerDisplay.Instance.AddPowerTimer((int)_type, _duration, _type);
            }
            Destroy(gameObject);
        }
    }
}