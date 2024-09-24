using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Power_ups
{
    public class PowerupGun : PowerupDrop
    {
        protected override void Collected(GameObject ball)
        {
            PaddleController.Instance.GunActivation(_duration);
            
            base.Collected(ball);
        }
    }
}