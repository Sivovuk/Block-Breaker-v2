using System;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.Bricks
{
    public class MovingBrick : BasicBrick
    {
        [SerializeField] private Transform _pointB;
        [SerializeField] private float _speed = 10f;


        private Vector3 _pointPosition;

        protected override void Start()
        {
            _pointPosition = _pointB.position;
            
            MoveToPoints();
        }

        void MoveToPoints()
        {
            transform.DOMove(_pointPosition, _speed)
                .SetLoops(-1, LoopType.Yoyo) // Set loops and loop type (Yoyo for ping-pong effect)
                .SetEase(Ease.Linear);
        }
    }
}