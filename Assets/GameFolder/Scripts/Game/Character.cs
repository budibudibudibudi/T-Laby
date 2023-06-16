using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UWAK.ITEM;
using UWAK.SCRIPTABLE;

namespace UWAK.GAME.PLAYER
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private int health = 100;
        public int GetHealth() { return health; }
        public delegate void OnHealthChange(int health);
        public OnHealthChange onHealthChange;

        [SerializeField] private int maxHealth = 100;
        public int GetMaxHealth() { return maxHealth; }

        [SerializeField] private float stamina;
        public float GetStamina() { return stamina; }
        public delegate void OnStaminaChange(float amount);
        public OnStaminaChange onStaminaChange;

        [SerializeField] private float maxStamina = 20;
        public float GetMaxStamina() { return maxStamina; }
        public delegate void OnMaxStaminaChange(float amount);
        public OnMaxStaminaChange onMaxStaminaChange;

        [SerializeField] ItemSlotClass[] currentItems;
        public delegate void OnInventoryChange(ItemSlotClass[] items);
        public OnInventoryChange onInventoryChange;
        public ItemSlotClass[] GetCurrentItems() { return currentItems; }

        [SerializeField] private int useItemIndex;
        public delegate void OnInventoryIndexChange(int index);
        public OnInventoryIndexChange onInventoryIndexChange;

        [SerializeField] Item itemHand;
        public Item GetItemHand() { return itemHand; }
        public delegate void OnHandChange(Item item);
        public OnHandChange onHandChange;
        private void Start()
        {
            currentItems = new ItemSlotClass[6];
        }
        public void AddMaxStamina(float amount)
        {
            maxStamina += amount;
            onMaxStaminaChange?.Invoke(maxStamina);
        }
        public void ItemOnHandChange(Item item)
        {
            itemHand = item;
            onHandChange?.Invoke(itemHand);
        }
        public void HealthChange(int amount)
        {
            Mathf.Clamp(health += amount,0,100);
            onHealthChange?.Invoke(health);
        }
        public void StaminaChange(float amount)
        {
            Mathf.Clamp(stamina += amount, 0, maxStamina);
            onStaminaChange?.Invoke(stamina);
        }
        public void InventoryUpdate(ItemSlotClass[] items)
        {
            currentItems = items;
            onInventoryChange?.Invoke(currentItems);
        }
        public void SetInventoryIndex(int index)
        {
            useItemIndex = index;
            onInventoryIndexChange?.Invoke(useItemIndex);
        }
    }
}