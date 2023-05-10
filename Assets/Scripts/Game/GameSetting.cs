using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UWAK.GAME
{
    public class GameSetting : MonoBehaviour
    {
        //private bool isGameOver;
        //public bool GetIsGameOver() { return isGameOver; }

        public static GameSetting Instance;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        private void OnEnable()
        {
            GameManager.Instance.onGameStateChange += OnGameStateChange;
        }
        private void OnDisable()
        {
            GameManager.Instance.onGameStateChange -= OnGameStateChange;
        }

        private void OnGameStateChange(GameState state)
        {
            switch (state)
            {
                case GameState.GAMEPAUSED:
                    Time.timeScale = 0;
                    break;
                case GameState.GAMERESUME:
                    Time.timeScale = 1;
                    break;
                default:
                    break;
            }
        }

    }
}
