using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UWAK.ITEM;

namespace UWAK.SCRIPTABLE
{
    [System.Serializable]
    public class ItemSlotClass
    {
        [SerializeField] Item item;
        [SerializeField] private int amount;
        public ItemSlotClass()
        {
            item = null;
            amount = 0;
        }
        public void AddItem(Item _item,int _amount)
        {
            item = _item;
            amount = _amount;
        }
        public void AddAmount(int _amount) { amount += _amount; }
        public void SubAmount(int _amount) { amount -= _amount; }
        public void Clear() {
            item = null;
            amount = 0;
        }
        public Item GetItem() { return item; }
        public int GetAmount() { return amount; }
    }
}
