using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UWAK.GAME.PLAYER;
using UWAK.ITEM;

namespace UWAK.GAME
{
    public class GameSetting : MonoBehaviour
    {
        [SerializeField] Player player;
        [SerializeField] Enemy enemy;

        [SerializeField] GameObject spawnRareItemLocParent;
        GameObject[] spawnRareItemLoc;
        [SerializeField] GameObject spawnLocParent;
        GameObject[] spawnItemLoc;
        [SerializeField] Item[] items;
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
            InitItemLoc();
            InitRareItemLoc();
            SpawnItems();
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
        private void SpawnItems()
        {
            for (int i = 0; i < items.Length; i++)
            {
                if(items[i].rareItem)
                {
                    GameObject location = GetLocation(spawnRareItemLoc);
                    if(location != null)
                    {
                        Instantiate(items[i].gameObject, location.transform);
                    }
                }
                else
                {
                    GameObject location = GetLocation(spawnItemLoc);
                    if(location != null)
                    {
                        Instantiate(items[i].gameObject, location.transform);
                    }
                }
            }
        }
        private void InitItemLoc()
        {
            spawnItemLoc = new GameObject[spawnLocParent.transform.childCount];
            for (int i = 0; i < spawnItemLoc.Length; i++)
            {
                spawnItemLoc[i] = spawnLocParent.transform.GetChild(i).gameObject;
            }
        }
        private void InitRareItemLoc()
        {
            spawnRareItemLoc = new GameObject[spawnRareItemLocParent.transform.childCount];
            for (int i = 0; i < spawnRareItemLoc.Length; i++)
            {
                spawnRareItemLoc[i] = spawnRareItemLocParent.transform.GetChild(i).gameObject;
            }
        }
        private GameObject GetLocation(GameObject[] types)
        {
            int random = UnityEngine.Random.Range(0, types.Length);
            if (types[random].transform.childCount == 0)
            {
                return types[random];
            }
            else
            {
                GetLocation(types);
            }
            return null;
        }
    }
}
