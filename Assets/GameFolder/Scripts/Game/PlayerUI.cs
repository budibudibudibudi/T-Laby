using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UWAK.SCRIPTABLE;
using UWAK.UI;

namespace UWAK.GAME.PLAYER
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] Slider healthBar;
        [SerializeField] Slider staminaBar;


        //public void SetStaminaBar(float amount) { staminaBar.value = amount; }

        #region SINGLETON
        public static PlayerUI Instance;
        private void Awake()
        {
            Instance = this;
        }
        #endregion
        #region subscription
        private void OnEnable()
        {
            Character.Instance.onHealthChange += onHealthChange;
            Character.Instance.onStaminaChange += onStaminaChange;
            Character.Instance.onMaxStaminaChange += OnMaxStaminaChange;
        }


        private void OnDisable()
        {
            Character.Instance.onHealthChange -= onHealthChange;
            Character.Instance.onStaminaChange -= onStaminaChange;
            Character.Instance.onMaxStaminaChange -= OnMaxStaminaChange;
        }
        #endregion
        private void Start()
        {
            healthBar.maxValue = Character.Instance.GetMaxHealth();
            healthBar.value = Character.Instance.GetHealth();
            staminaBar.maxValue = Character.Instance.GetStamina();
            staminaBar.value = Character.Instance.GetStamina();

        }

        private void onHealthChange(int health)
        {
            healthBar.value = health;
        }

        private void onStaminaChange(float amount)
        {
            staminaBar.value = amount;
        }

        private void OnMaxStaminaChange(float amount)
        {
            staminaBar.maxValue = amount;
        }
    }
}
