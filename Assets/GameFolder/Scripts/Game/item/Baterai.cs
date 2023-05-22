using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UWAK.GAME.PLAYER;

namespace UWAK.ITEM
{
    public class Baterai : Item
    {
        public override void Use()
        {
            base.Use();
            Item temp = Player.Instance.GetItemInHand();
            temp.GetSenter().AddBateraiValue(temp.GetSenter().GetMaxBateraiValue());
            Destroy(gameObject);
        }
        public override Senter GetSenter()
        {
            return null;
        }
    }
}