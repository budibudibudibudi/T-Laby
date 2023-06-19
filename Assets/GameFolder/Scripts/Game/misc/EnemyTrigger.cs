using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UWAK.GAME.TRIGGER
{
    public class EnemyTrigger : MonoBehaviour
    {
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
                case GameState.GAME:
                    GetComponent<Collider>().enabled = true;
                    break;
                case GameState.GAMERESUME:
                    GetComponent<Collider>().enabled = true;
                    break;
                case GameState.CHASEEVENT:
                    GetComponent<Collider>().enabled = false;
                    break;
                default:
                    break;
            }
        }


        [SerializeField] private int Index;
        private void OnTriggerEnter(Collider other)
        {
            int random = UnityEngine.Random.Range(0, 100);
            if (random <= 50)
            {
                if (other.CompareTag("Player"))
                {
                    Actions.TriggerIndex.Invoke(Index);
                }
            }
        }
    }
}