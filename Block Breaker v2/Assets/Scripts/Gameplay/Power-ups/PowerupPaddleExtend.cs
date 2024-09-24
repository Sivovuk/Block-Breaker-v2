using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Power_ups
{
    public class PowerupPaddleExtend : PowerupDrop
    {
        protected override void Collected(GameObject ball)
        {
            PaddleController.Instance.PaddleExtend(_duration);
            
            base.Collected(ball);
        }
    }
}