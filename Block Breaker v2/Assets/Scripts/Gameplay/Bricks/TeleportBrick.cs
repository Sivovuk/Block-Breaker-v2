using UnityEngine;

namespace Gameplay.Bricks
{
    public class TeleportBrick : BasicBrick
    {
        private bool _isTeleported;
        
        private Camera _mainCamera;

        protected override void Start()
        {
            base.Start();
            _mainCamera = Camera.main;
        }

        public override void TakeDamage(bool isFireball)
        {
            if (!_isTeleported)
            {
                _isTeleported = true;
                Teleport();
                return;
            }
            
            base.TakeDamage(isFireball);
        }

        private void Teleport()
        {
            Vector3 minScreenBoundsX = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, _mainCamera.nearClipPlane));
            Vector3 maxScreenBoundsX = _mainCamera.ViewportToWorldPoint(new Vector3(1, 0, _mainCamera.nearClipPlane));
            Vector3 maxScreenBoundsY = _mainCamera.ViewportToWorldPoint(new Vector3(0, 1, _mainCamera.nearClipPlane));

            float width = maxScreenBoundsX.x - minScreenBoundsX.x;
            float height = maxScreenBoundsY.y - minScreenBoundsX.y;

            float newHeight = height * 0.75f;

            float newMinScreenBoundsY = maxScreenBoundsY.y - newHeight;
            maxScreenBoundsX.x = maxScreenBoundsX.x - (transform.localScale.x/2);
            minScreenBoundsX.x = minScreenBoundsX.x + (transform.localScale.x/2);
            maxScreenBoundsY.y = maxScreenBoundsY.y - (transform.localScale.y/2);

            Vector3 newPosition = new Vector3(
                Random.Range(minScreenBoundsX.x, maxScreenBoundsX.x), 
                Random.Range(newMinScreenBoundsY, maxScreenBoundsY.y), 
                0);

            transform.position = newPosition;
        }
    }
}