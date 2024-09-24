using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BricksType
{
    Basic = 1,
    MultiHit = 2,
    Stone = 3,
    Moving = 4,
    Teleport = 5,
    Invisible = 5
}

namespace Gameplay.Bricks
{
    public class BasicBrick : MonoBehaviour, IHit
    {
        [SerializeField] protected int _brickLife = 1;
        [SerializeField] private int _brickBreakReward = 10;
        [field: SerializeField] public BricksType Type { get; private set; }

        protected SpriteRenderer _sprite;

        protected LevelController _levelController;
        
        protected virtual void Start()
        {
            _sprite = GetComponent<SpriteRenderer>();
        }

        public virtual void Setup(LevelController levelController)
        {
            _levelController = levelController;
        }

        public virtual void TakeDamage(bool isFireball)
        {
            if (isFireball)
            {
                _levelController.BrickDestroy(transform.position, _brickBreakReward);
                Destroy(gameObject);
                return;
            }

            _brickLife--;

            if (_brickLife < 1)
            {
                _levelController.BrickDestroy(transform.position, _brickBreakReward);
                Destroy(gameObject);
                return;
            }

            ChangeVisual();
        }

        protected virtual void ChangeVisual()
        {
            float a = _sprite.color.a * 0.5f;
            _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, a);
        }
    }
}