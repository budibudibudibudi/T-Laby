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
            Character.Instance.HealthChange(50);
            Destroy(gameObject);
        }
    }

}
