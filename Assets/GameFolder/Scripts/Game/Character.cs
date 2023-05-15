using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UWAK.GAME.PLAYER
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private int capsuleHealth = 3;
        public int GetCapsuleHealth() { return capsuleHealth; }
        public delegate void OnCapsuleHealthChange(int amount);
        public OnCapsuleHealthChange onCapsuleHealthChange;

        [SerializeField] private int health = 100;
        public int GetHealth() { return health; }
        public delegate void OnHealthChange(int health);
        public OnHealthChange onHealthChange;

        [SerializeField] private float stamina = 20;
        public float GetStamina() { return stamina; }
        public delegate void OnStaminaChange(float amount);
        public OnStaminaChange onStaminaChange;
       // public void SetStamina(float amount) { Mathf.Clamp(stamina += amount,0,20); }
        
        public void UseHeal(int amount)
        {
            if (health < 100)
                capsuleHealth -= amount;
            else
                return;
            HealthChange(40);
            onCapsuleHealthChange?.Invoke(capsuleHealth);
        }

        public void HealthChange(int amount)
        {
            Mathf.Clamp(health += amount,0,100);
            onHealthChange?.Invoke(health);
            if(health >0)
            {
                GameManager.Instance.ChangeState(GameState.LOSE);
            }
        }
        public void StaminaChange(float amount)
        {
            Mathf.Clamp(stamina += amount, 0, 20);
            onStaminaChange?.Invoke(stamina);
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
        private void Start()
        {
        }
    }
}