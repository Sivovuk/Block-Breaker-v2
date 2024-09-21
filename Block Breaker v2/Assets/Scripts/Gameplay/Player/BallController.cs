using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{ 
    [SerializeField] private float _pushForce = 5f; 
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
        
    }

    public void ReleaseBall()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody2D.AddForce(Vector2.up * _pushForce, ForceMode2D.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Paddle"))
        {
            AddVelocity();
        }
        else if (collision.gameObject.CompareTag("Brick") && collision.gameObject.TryGetComponent<BasicBrick>(out BasicBrick brick))
        {
            AddVelocity();
            brick.TakeDamage();
        }
    }

    private void AddVelocity()
    {
        float x = 0;
        float y = 0;

        if (_rigidbody2D.velocity.x < 20)
            x += 1f;
        if (_rigidbody2D.velocity.y < 20)
            y += 1f;
        
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x + x, _rigidbody2D.velocity.y + y);
    }
}
