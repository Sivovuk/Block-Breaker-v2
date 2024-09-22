using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Power_ups
{
    public class PowerupGun : PowerupDrop
    {
        protected override void Collected(GameObject ball)
        {
            StartCoroutine(PaddleController.Instance.GunActivationCorutine(_duration));
            
            base.Collected(ball);
        }
    }
}