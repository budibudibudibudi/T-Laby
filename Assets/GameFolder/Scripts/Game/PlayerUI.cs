using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UWAK.GAME.PLAYER
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] TMPro.TMP_Text capsuleHealthText;
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
        private void OnEnable()
        {
            Character.Instance.onCapsuleHealthChange += onCapsuleHealthChange;
            Character.Instance.onHealthChange += onHealthChange;
            Character.Instance.onStaminaChange += onStaminaChange;
        }


        private void OnDisable()
        {
            Character.Instance.onCapsuleHealthChange -= onCapsuleHealthChange;
            Character.Instance.onHealthChange -= onHealthChange;
            Character.Instance.onStaminaChange -= onStaminaChange;
        }
        private void Start()
        {
            healthBar.maxValue = Character.Instance.GetHealth();
            healthBar.value = Character.Instance.GetHealth();
            capsuleHealthText.text = $"Capsule : {Character.Instance.GetCapsuleHealth()}";
            staminaBar.maxValue = Character.Instance.GetStamina();
            staminaBar.value = Character.Instance.GetStamina();
        }

        private void onHealthChange(int health)
        {
            healthBar.value = health;
        }

        private void onCapsuleHealthChange(int amount)
        {
            capsuleHealthText.text = $"Capsule : {amount}";
        }
        private void onStaminaChange(float amount)
        {
            staminaBar.value = amount;
        }

    }
}
