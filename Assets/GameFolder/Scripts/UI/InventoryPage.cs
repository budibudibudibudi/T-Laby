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
        private void Start()
        {
            inventUI = new GameObject[inventUIParent.transform.childCount];
            for (int i = 0; i < inventUI.Length; i++)
            {
                inventUI[i] = inventUIParent.transform.GetChild(i).gameObject;
            }
        }
        private void OnEnable()
        {
            Character.Instance.onInventoryIndexChange += onInventoryIndexChange;
            Character.Instance.onInventoryChange += onInventoryChange;
        }
        private void OnDisable()
        {
            Character.Instance.onInventoryIndexChange -= onInventoryIndexChange;
            Character.Instance.onInventoryChange -= onInventoryChange;
        }
        private void onInventoryChange(ItemSlotClass[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                try
                {
                    inventUI[i].GetComponent<Image>().sprite = items[i].GetItem().Icon;
                    inventUI[i].transform.GetChild(0).GetComponent<TMP_Text>().text = (i+1).ToString();
                }
                catch
                {
                    inventUI[i].GetComponent<Image>().sprite = null;
                    inventUI[i].transform.GetChild(0).GetComponent<TMP_Text>().text = "";
                }
            }
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