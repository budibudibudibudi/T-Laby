using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UWAK.GAME.PLAYER;
using UWAK.SCRIPTABLE;

namespace UWAK.UI
{
    public class InventoryPage : Page
    {
        [SerializeField] GameObject inventUIParent;
        GameObject[] inventUI;
        private void Awake()
        {
            inventUI = new GameObject[inventUIParent.transform.childCount];
            for (int i = 0; i < inventUI.Length; i++)
            {
                inventUI[i] = inventUIParent.transform.GetChild(i).gameObject;
            }
        }
        private void OnEnable()
        {
            RefreshUI();
            Player.Instance.onInventoryIndexChange += onInventoryIndexChange;
        }

        private void RefreshUI()
        {
            ItemSlotClass[] items = Player.Instance.GetCurrentItems();
            for (int i = 0; i < items.Length; i++)
            {
                try
                {
                    inventUI[i].GetComponent<Image>().sprite = items[i].GetItem().Icon;
                }
                catch
                {
                    inventUI[i].GetComponent<Image>().sprite = null;
                }
            }
        }

        private void OnDisable()
        {
            Player.Instance.onInventoryIndexChange -= onInventoryIndexChange;
        }
        private void onInventoryChange(ItemSlotClass[] items)
        {
        }

        private void onInventoryIndexChange(int index)
        {
            foreach (var item in inventUI)
            {
                item.GetComponent<Outline>().enabled = false;
            }
            inventUI[index].GetComponent<Outline>().enabled = true;
        }
    }
}