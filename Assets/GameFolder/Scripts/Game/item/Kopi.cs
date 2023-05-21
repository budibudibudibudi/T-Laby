
using UWAK.GAME.PLAYER;

namespace UWAK.ITEM
{
    public class Kopi : Item
    {
        public override void Use()
        {
            base.Use();
            Character.Instance.AddMaxStamina(1);
        }
    }
}
