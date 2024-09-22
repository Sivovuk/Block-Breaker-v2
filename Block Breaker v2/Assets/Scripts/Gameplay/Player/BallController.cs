using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Bricks;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Player
{
    public class BallController : MonoBehaviour, ICollectable
    {
        [SerializeField] private float _pushForce = 5f;
        [SerializeField] private Vector3 _velocity;
        private Vector3 _direction;

        private float _ballRadius;

        private bool _collisionDetection;

        private PaddleController _paddleController;
        private Rigidbody2D _rigidbody2D;

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _paddleController = GetComponentInParent<PaddleController>();
            _direction = Random.insideUnitSphere.normalized; // Set a random initial direction
            _ballRadius = GetComponent<CircleCollider2D>().radius;
        }

        void Update()
        {
            _velocity = _rigidbody2D.velocity;
        }

        public void ReleaseBall()
        {
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody2D.AddForce(Vector2.up * _pushForce, ForceMode2D.Force);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(_paddleController.BallLocked) return;
            
            if (collision.gameObject.CompareTag("Paddle"))
            {
                AddVelocity();
            }
            else if (collision.gameObject.CompareTag("Brick") &&
                     collision.gameObject.TryGetComponent<BasicBrick>(out BasicBrick brick))
            {
                AddVelocity();
                brick.TakeDamage();
            }
        }

        private void AddVelocity()
        {
            float x = 0;
            float y = 0;

            if (Mathf.Abs(_rigidbody2D.velocity.x) < 20)
                x = 0.5f;
            if (Mathf.Abs(_rigidbody2D.velocity.y) < 20)
                y = 0.5f;

            if (_rigidbody2D.velocity.x < 0)
                x = -x;
            if (_rigidbody2D.velocity.y < 0)
                y = -y;
                
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x + x, _rigidbody2D.velocity.y + y);
        }

        public void Collect(CollectableType type)
        {
            Debug.Log(type);
        }
    }
}