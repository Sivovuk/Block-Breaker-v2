using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Bricks;
using Gameplay.Power_ups;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Player
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private float _pushForce = 5f;
        [SerializeField] private Vector3 _velocity;
        
        private Vector3 _direction;

        private float _ballRadius;

        private bool _collisionDetection;
        private bool _isFireball;
        public bool IsBomb { get; private set; }

        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _sprite;
        [SerializeField] private Collider2D _fireballCollider;
        [SerializeField] private Bomb _bomb;

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _direction = Random.insideUnitSphere.normalized; // Set a random initial direction
            _ballRadius = GetComponent<CircleCollider2D>().radius;
            _sprite = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (PaddleController.Instance.BallLocked)
            {
                transform.position = new Vector3(PaddleController.Instance.transform.position.x, PaddleController.Instance.transform.position.y+1.5f, 0);
            }
        }

        private void FixedUpdate()
        {
            _velocity = _rigidbody2D.velocity;
        }

        public void ReleaseBall()
        {
            List<Vector2> _directions = new List<Vector2>(); 
            _directions.Add(new Vector2(0, 1));   // Up
            _directions.Add(new Vector2(1, 1));   // Diagonal up right
            _directions.Add(new Vector2(-1, -1));  // Diagonal up left
            
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody2D.AddForce(_directions[Random.Range(0, _directions.Count)] * _pushForce, ForceMode2D.Force);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Brick") &&
                collider.TryGetComponent<BasicBrick>(out BasicBrick brick))
            {
                brick.TakeDamage(_isFireball);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(PaddleController.Instance.BallLocked) return;
                
            if (collision.gameObject.CompareTag("Paddle"))
            {
                //AddVelocity();
            }
            else if (collision.gameObject.CompareTag("Brick") &&
                     collision.gameObject.TryGetComponent<BasicBrick>(out BasicBrick brick))
            {
                brick.TakeDamage(_isFireball);
                if (IsBomb)
                {
                    IsBomb = false;
                    GameObject spawn = Instantiate(_bomb.gameObject, transform.position, Quaternion.identity);
                    Destroy(spawn,1f);
                }
                
            }

            AddVelocity();
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

        public IEnumerator FireballCorutine(float duration)
        {
            _fireballCollider.enabled = true;
            _isFireball = true;
            _sprite.color = Color.red;
            yield return new WaitForSeconds(duration);
            _fireballCollider.enabled = false;
            _isFireball = false;
            _sprite.color = Color.white;
        }

        public void SetBallAsBomb()
        {
            IsBomb = true;
        }
    }
}