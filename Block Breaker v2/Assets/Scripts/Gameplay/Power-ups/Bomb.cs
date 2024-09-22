using System;
using Gameplay.Bricks;
using UnityEngine;

namespace Gameplay.Power_ups
{
    public class Bomb : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Brick") &&
                collider.TryGetComponent<BasicBrick>(out BasicBrick brick))
            {
                brick.TakeDamage(true);
            }
        }
    }
}