using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UWAK.GAME.PLAYER;

namespace UWAK.ITEM
{
    public class NasiBungkus : Item
    {
        public override void Use()
        {
            base.Use();
            Player.Instance.HealthChange(50);
            Player.Instance.SetState(PLAYERSTATE.EAT);
            Destroy(gameObject);
        }
        public override Senter GetSenter()
        {
            return null;
        }
    }

}
