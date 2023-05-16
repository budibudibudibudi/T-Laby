using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UWAK.SCRIPTABLE;
using UWAK.ITEM;

namespace UWAK.GAME.PLAYER
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] ItemSlotClass[] currentItems = new ItemSlotClass[4];

        #region SINGLETON
        public static InventoryManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        #endregion

        private void Start()
        {
            for (int i = 0; i < currentItems.Length; i++)
            {
                currentItems[i] = new ItemSlotClass();
            }
        }

        public void AddItem(Item item,int amount)
        {
            ItemSlotClass temp = Contains(item);
            if(temp!=null)
            {
                if(temp.GetItem().isInventoryItem)
                {
                    if(temp.GetItem().isStackAble)
                    {
                        for (int i = 0; i < currentItems.Length; i++)
                        {
                            currentItems[i].AddItem(item, amount);
                        }
                    }
                }
            }
            else
            {
                if(temp.GetItem().isInventoryItem)
                {
                    for (int i = 0; i < currentItems.Length; i++)
                    {
                        currentItems[i].AddItem(item, amount);
                    }
                }
            }
            Character.Instance.InventoryUpdate(currentItems);
        }
        public void SubItem(Item item, int amount)
        {
            ItemSlotClass temp = Contains(item);
            if(temp!=null)
            {
                if(temp.GetItem().isInventoryItem)
                {
                    if(temp.GetAmount()>1)
                    {
                        temp.SubAmount(amount);
                    }
                    else
                    {
                        int slotRemove = 0;
                        for (int i = 0; i < currentItems.Length; i++)
                        {
                            if(currentItems[i].GetItem() == item)
                            {
                                slotRemove = i;
                                break;
                            }
                        }
                        currentItems[slotRemove].Clear();
                    }
                }
            }
            Character.Instance.InventoryUpdate(currentItems);
        }
        public ItemSlotClass Contains(Item item)
        {
            for (int i = 0; i < currentItems.Length; i++)
            {
                if (currentItems[i].GetItem() == item)
                    return currentItems[i];
            }
            return null;
        }
    }
}
