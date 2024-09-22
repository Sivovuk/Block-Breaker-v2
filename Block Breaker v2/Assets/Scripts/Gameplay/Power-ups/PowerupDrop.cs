using System;
using Gameplay.Player;
using UnityEngine;

public enum CollectableType
{
    Multiply,
    PaddleExtend,
    Fireball,
    Bomb,
    Gun
}

namespace Gameplay.Power_ups
{
    public class PowerupDrop : MonoBehaviour
    {
        [SerializeField] protected float _fallSpeed = 10f;
        [SerializeField] protected float _duration;
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
                PaddleController.Instance.Collect(_type, _duration);
                Collected(PaddleController.Instance.BallController.gameObject);
            }
        }

        protected virtual void Collected(GameObject ball)
        {
            Debug.Log("Collected");
            
            Destroy(gameObject);
        }
    }
}