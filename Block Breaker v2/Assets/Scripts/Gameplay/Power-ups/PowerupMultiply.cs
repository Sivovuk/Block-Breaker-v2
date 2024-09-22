using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Power_ups
{
    public class PowerupMultiply : PowerupDrop
    {
        [SerializeField] private int _multiplyValue = 3;
        [SerializeField] private int _pushForce = 100;
        
        private List<Vector2> _directions = new List<Vector2>(); 
        protected override void Start()
        {
            base.Start();
            _directions.Add(new Vector3(0, 1, 0));   // Up
            _directions.Add(new Vector3(1, 1, 0));   // Diagonal up right
            _directions.Add(new Vector3(1, 0, 0));   // Right
            _directions.Add(new Vector3(1, -1, 0));  // Diagonal down right
            _directions.Add(new Vector3(0, -1, 0));  // Down
            _directions.Add(new Vector3(-1, 1, 0));  // Diagonal down left
            _directions.Add(new Vector3(-1, 0, 0));  // left
            _directions.Add(new Vector3(-1, -1, 0));  // Diagonal up left
        }

        protected override void Collected(GameObject ball)
        {
            for (int i = 0; i < _multiplyValue; i++)
            {
                GameObject spawn = Instantiate(ball.gameObject, ball.transform.position,
                    Quaternion.identity);
                
                spawn.TryGetComponent(out Rigidbody2D rb);
                if (rb != null)
                {
                    rb.AddForce(_directions[Random.Range(0, _directions.Count-1)] * _pushForce, ForceMode2D.Force);
                }
            }
            
            base.Collected(ball);
        }
    }
}