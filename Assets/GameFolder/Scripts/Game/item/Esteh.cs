
using UWAK.GAME.PLAYER;

namespace UWAK.ITEM
{
    public class Esteh : Item
    {
        public override void Use()
        {
            base.Use();
            Player.Instance.HealthChange(25);
            Player.Instance.SetState(PLAYERSTATE.DRINK);
            Destroy(gameObject);
        }
        public override Senter GetSenter()
        {
            return null;
        }
    }
}