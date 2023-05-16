using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UWAK.GAME.PLAYER;

namespace UWAK.GAME
{
    public class GameSetting : MonoBehaviour
    {
        [SerializeField] Player player;
        [SerializeField] Enemy enemy;

        #region Singleton
        public static GameSetting Instance;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        #endregion
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
                case GameState.WIN:
                    //audio
                    break;
                case GameState.LOSE:
                    player.Death();
                    //audio
                    break;
                default:
                    break;
            }
        }


    }
}
