using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UWAK.GAME.ENEMY;
using UWAK.GAME.PLAYER;
using UWAK.GAME.TRIGGER;
using UWAK.ITEM;
using UWAK.SAVELOAD;

namespace UWAK.GAME
{
    public class GameSetting : MonoBehaviour
    {
        [SerializeField] Player player;
        [SerializeField] EnemyController enemy;

        [SerializeField] GameObject spawnRareItemLocParent;
        GameObject[] spawnRareItemLoc;
        [SerializeField] GameObject spawnLocParent;
        GameObject[] spawnItemLoc;
        [SerializeField] Item[] items;

        [SerializeField] EnemyTriggerManager[] triggerSlots;
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
            LoadData();
        }
        private void OnEnable()
        {
            GameManager.Instance.onGameStateChange += OnGameStateChange;
            Actions.TriggerIndex += Triggered;
        }

        private void OnDisable()
        {
            GameManager.Instance.onGameStateChange -= OnGameStateChange;
            Actions.TriggerIndex -= Triggered;
        }

        private void Triggered(int obj)
        {
            enemy.gameObject.SetActive(true);
            enemy.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            enemy.transform.position = triggerSlots[obj].spawnLoc.position;
            enemy.gameObject.GetComponent<NavMeshAgent>().enabled = true;
            enemy.SetState(EnemyState.PATROL);
            GameManager.Instance.ChangeState(GameState.CHASEEVENT);
        }


        private void LoadData()
        {
            SaveName _isNewGame = SaveAndLoad.ReadFromJSON<SaveName>("NewGame");
            switch (_isNewGame)
            {
                case SaveName.NULL:
                    SaveAndLoad.SaveToJSON(SaveName.LEVEL1, "NewGame");
                    GameManager.Instance.ChangeState(GameState.OPENGUIDE);
                    break;
                case SaveName.LEVEL1:
                    break;
                case SaveName.LEVEL2:
                    break;
                case SaveName.LEVEL3:
                    break;
                default:
                    break;
            }
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
