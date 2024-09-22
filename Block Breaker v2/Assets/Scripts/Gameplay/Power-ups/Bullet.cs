using System;
using Gameplay.Bricks;
using UnityEngine;

namespace Gameplay.Power_ups
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private void Update()
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            if (collider2D.CompareTag("Brick") && collider2D.TryGetComponent<BasicBrick>(out BasicBrick brick))
            {
                brick.TakeDamage(false);
            }
            
            Destroy(gameObject);
        }
    }
}