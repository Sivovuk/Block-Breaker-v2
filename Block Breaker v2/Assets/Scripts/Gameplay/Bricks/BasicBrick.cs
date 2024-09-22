using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Bricks
{
    public class BasicBrick : MonoBehaviour, IHit
    {
        [SerializeField] protected int _brickLife = 1;

        protected SpriteRenderer _sprite;
        
        protected virtual void Start()
        {
            _sprite = GetComponent<SpriteRenderer>();
        }

        public virtual void TakeDamage()
        {
            _brickLife--;

            if (_brickLife < 1)
            {
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