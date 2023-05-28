using StarterAssets;
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UWAK.ITEM;
using UWAK.SCRIPTABLE;

namespace UWAK.GAME.PLAYER
{
    public class Player : MonoBehaviour
    {
        [SerializeField] GameObject Camera;
        [SerializeField] GameObject itemParent;
        [SerializeField] GameObject Hand;
        [SerializeField] Item[] itemInHand;

        DepthOfField depthOfField;

        private int curentHealth;

        #region SINGLETON
        public static Player Instance;
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
            Volume volumeCamera = Camera.GetComponent<Volume>();

            curentHealth = Character.Instance.GetHealth();

            volumeCamera.profile.TryGet(out depthOfField);
            depthOfField.mode.overrideState = true;
            depthOfField.mode.value = DepthOfFieldMode.Bokeh;
            depthOfField.focusDistance.value = 10f;

            itemInHand = new Item[Hand.transform.childCount];
            for (int i = 0; i < itemInHand.Length; i++)
            {
                itemInHand[i] = Hand.transform.GetChild(i).GetComponent<Item>();
            }
        }
        private void OnEnable()
        {
            GameManager.Instance.onGameStateChange +=OnGameStateChange;
            Character.Instance.onHealthChange += OnHealthChange;
            Character.Instance.onHandChange += OnHandChange;
        }

        private void OnDisable()
        {
            GameManager.Instance.onGameStateChange -= OnGameStateChange;
            Character.Instance.onHealthChange -= OnHealthChange;
            Character.Instance.onHandChange -= OnHandChange;
        }

        private void OnGameStateChange(GameState state)
        {
            switch (state)
            {
                case GameState.WIN:
                    break;
                case GameState.LOSE:
                    break;
                case GameState.GAMEPAUSED:
                    depthOfField.focusDistance.value = 0.1f;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case GameState.GAMERESUME:
                    depthOfField.focusDistance.value = 10f;
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
                case GameState.OPENINVENTORY:
                    depthOfField.focusDistance.value = 0.1f;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                default:
                    break;
            }
        }

        private void OnHandChange(Item _itemInHand)
        {
            foreach (var items in itemInHand)
            {
                items.gameObject.SetActive(false);
            }
            if (_itemInHand == null)
                return;
            else
            {
                for (int i = 0; i < itemInHand.Length; i++)
                {
                    if (itemInHand[i].itemName == _itemInHand.itemName)
                    {
                        itemInHand[i].gameObject.SetActive(true);
                    }
                }

            }
        }
        private void OnHealthChange(int health)
        {
            if(health<curentHealth)
            {
                //animasi hitted
            }
            else if(health>curentHealth)
            {
                //animasi healed
            }
            curentHealth = health;
            if(curentHealth <= 0)
            {
                GameManager.Instance.ChangeState(GameState.LOSE);
            }
        }
        public void Death()
        {
            //animasi death jg
        }

        public Item GetItemInHand()
        {
            for (int i = 0; i < itemInHand.Length; i++)
            {
                if(itemInHand[i].gameObject.activeInHierarchy)
                {
                    return itemInHand[i];
                }
            }
            return null;
        }
    }

}
