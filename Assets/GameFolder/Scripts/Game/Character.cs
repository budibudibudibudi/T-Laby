using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UWAK.SCRIPTABLE;

namespace UWAK.GAME.PLAYER
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private int capsuleHealth = 3;
        public int GetCapsuleHealth() { return capsuleHealth; }
        public delegate void OnCapsuleHealthChange(int amount);
        public OnCapsuleHealthChange onHealUsed;

        [SerializeField] private int health = 100;
        public int GetHealth() { return health; }
        public delegate void OnHealthChange(int health);
        public OnHealthChange onHealthChange;

        [SerializeField] private float stamina = 20;
        public float GetStamina() { return stamina; }
        public delegate void OnStaminaChange(float amount);
        public OnStaminaChange onStaminaChange;

        [SerializeField] ItemSlotClass[] currentItems = new ItemSlotClass[4];
        public delegate void OnInventoryChange(ItemSlotClass[] items);
        public OnInventoryChange onInventoryChange;

        [SerializeField] private int inventoryIndex;
        public delegate void OnInventoryIndexChange(int index);
        public OnInventoryIndexChange onInventoryIndexChange;

        public void UseHeal(int amount)
        {
            if (health < 100)
            {
                capsuleHealth -= amount;
                onHealUsed?.Invoke(capsuleHealth);
            }
            else
                return;
        }

        public void HealthChange(int amount)
        {
            Mathf.Clamp(health += amount,0,100);
            onHealthChange?.Invoke(health);
        }
        public void StaminaChange(float amount)
        {
            Mathf.Clamp(stamina += amount, 0, 20);
            onStaminaChange?.Invoke(stamina);
        }
        public void InventoryUpdate(ItemSlotClass[] items)
        {
            currentItems = items;
            onInventoryChange?.Invoke(currentItems);
        }
        public void SetInventoryIndex(int index)
        {
            inventoryIndex = index;
            onInventoryIndexChange?.Invoke(inventoryIndex);
        }
        #region singleton
        public static Character Instance;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        #endregion

    }
}