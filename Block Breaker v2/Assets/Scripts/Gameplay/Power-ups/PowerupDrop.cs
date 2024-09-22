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
        [SerializeField] private float _fallSpeed;
        [SerializeField] private CollectableType _type;
        
        private Camera _mainCamera;
        private Vector3 _minScreenBounds;

        private void Start()
        {
            _mainCamera = Camera.main;
            _minScreenBounds = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, _mainCamera.nearClipPlane));
            
        }

        private void Update()
        {
            transform.Translate(Vector3.down * _fallSpeed * Time.deltaTime);
            
            if (transform.position.y-2 < _minScreenBounds.y)
                Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            if (collider2D.CompareTag("Ball") && collider2D.TryGetComponent<BallController>(out BallController ball))
                ball.Collect(_type);
        }
    }
}