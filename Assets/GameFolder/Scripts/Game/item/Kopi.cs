
using UWAK.GAME.PLAYER;

namespace UWAK.ITEM
{
    public class Kopi : Item
    {
        public override void Use()
        {
            base.Use();
            Player.Instance.AddMaxStamina(1);
            Destroy(gameObject);
        }
        public override Senter GetSenter()
        {
            return null;
        }
    }
}
