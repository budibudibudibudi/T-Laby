
using UWAK.GAME.PLAYER;

namespace UWAK.ITEM
{
    public class Esteh : Item
    {
        public override void Use()
        {
            base.Use();
            Character.Instance.HealthChange(25);
            Destroy(gameObject);
        }
        public override Senter GetSenter()
        {
            return null;
        }
    }
}