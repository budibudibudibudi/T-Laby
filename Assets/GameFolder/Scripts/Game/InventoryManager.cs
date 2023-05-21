using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UWAK.SCRIPTABLE;
using UWAK.ITEM;
using System;
using UWAK.UI;

namespace UWAK.GAME.PLAYER
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] ItemSlotClass[] currentItems;
        [SerializeField] GameObject itemParent;

        #region SINGLETON
        public static InventoryManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        #endregion
        private void OnEnable()
        {
            Character.Instance.onInventoryIndexChange += OnInventoryIndexChange;
        }

        private void OnDisable()
        {
            Character.Instance.onInventoryIndexChange -= OnInventoryIndexChange;
        }

        private void Start()
        {
            currentItems = new ItemSlotClass[6];
            for (int i = 0; i < currentItems.Length; i++)
            {
                currentItems[i] = new ItemSlotClass();
            }
        }

        private void OnInventoryIndexChange(int index)
        {
            if(GameManager.Instance.GetGameState() == GameState.OPENINVENTORY)
            {
                if (currentItems[index].GetItem() != null)
                {
                    if (currentItems[index].GetItem().isInventoryItem)
                    {
                        switch (currentItems[index].GetItem().itemName)
                        {
                            case NamaItem.KUNCI:
                                Character.Instance.ItemOnHandChange(currentItems[index].GetItem());
                                currentItems[index].GetItem().Use();
                                GameManager.Instance.ChangeState(GameState.GAMERESUME);
                                break;
                            case NamaItem.KOPI:
                                currentItems[index].GetItem().Use();
                                SubItem(currentItems[index].GetItem(), 1);
                                break;
                            case NamaItem.NASIBUNGKUS:
                                currentItems[index].GetItem().Use();
                                SubItem(currentItems[index].GetItem(), 1);
                                break;
                            case NamaItem.ESTEH:
                                currentItems[index].GetItem().Use();
                                SubItem(currentItems[index].GetItem(), 1);
                                break;
                            case NamaItem.BATERAI:
                                currentItems[index].GetItem().Use();
                                SubItem(currentItems[index].GetItem(), 1);
                                break;
                            case NamaItem.SENTER:
                                Character.Instance.ItemOnHandChange(currentItems[index].GetItem());
                                GameManager.Instance.ChangeState(GameState.GAMERESUME);
                                break;
                            default:
                                break;
                        }
                    }
                }

            }
            Character.Instance.InventoryUpdate(currentItems);
        }

        public void AddItem(Item item,int amount)
        {
            ItemSlotClass temp = Contains(item);
            if(temp!=null)
            {
                if(temp.GetItem().isInventoryItem)
                {
                    for (int i = 0; i < currentItems.Length; i++)
                    {
                        if(currentItems[i].GetItem() == null)
                        {
                            currentItems[i].AddItem(item, amount);
                            currentItems[i].GetItem().transform.SetParent(itemParent.transform);
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < currentItems.Length; i++)
                {
                    if (currentItems[i].GetItem() == null)
                    {
                        currentItems[i].AddItem(item, amount);
                        currentItems[i].GetItem().transform.SetParent(itemParent.transform);
                        break;
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
