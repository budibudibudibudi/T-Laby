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
            Player.Instance.onHealthChange += onHealthChange;
            Player.Instance.onStaminaChange += onStaminaChange;
            Player.Instance.onMaxStaminaChange += OnMaxStaminaChange;
        }


        private void OnDisable()
        {
            Player.Instance.onHealthChange -= onHealthChange;
            Player.Instance.onStaminaChange -= onStaminaChange;
            Player.Instance.onMaxStaminaChange -= OnMaxStaminaChange;
        }
        #endregion
        private void Start()
        {
            healthBar.maxValue = Player.Instance.GetMaxHealth();
            healthBar.value = Player.Instance.GetHealth();
            staminaBar.maxValue = Player.Instance.GetStamina();
            staminaBar.value = Player.Instance.GetStamina();

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
