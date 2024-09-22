using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Power_ups
{
    public class PowerupPaddleExtend : PowerupDrop
    {
        protected override void Collected(GameObject ball)
        {
            StartCoroutine(PaddleController.Instance.PaddleExtendCorutine(_duration));
            
            base.Collected(ball);
        }
    }
}