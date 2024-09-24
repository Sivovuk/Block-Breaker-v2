namespace Gameplay.Bricks
{
    public class InvisibleBrick : BasicBrick
    {
        private bool _isInvisible = true;
        
        public override void TakeDamage(bool isFireball)
        {
            if (_isInvisible)
            {
                _isInvisible = false;
                _sprite.enabled = true;
                return;
            }
            
            base.TakeDamage(isFireball);
        }
    }
}