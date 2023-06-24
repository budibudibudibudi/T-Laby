using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UWAK.ITEM;
using UWAK.SCRIPTABLE;

namespace UWAK.GAME.PLAYER
{
    public class Player : Character
    {
        [SerializeField] GameObject Camera;
        [SerializeField] GameObject itemParent;
        [SerializeField] GameObject Hand;
        [SerializeField] Item[] itemInHand;

        DepthOfField depthOfField;
        Vignette vignette;
        Animator anim;

        [SerializeField]private int curentHealth;

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
            volumeCamera.profile.TryGet(out depthOfField);
            volumeCamera.profile.TryGet(out vignette);
            depthOfField.mode.overrideState = true;
            depthOfField.mode.value = DepthOfFieldMode.Bokeh;
            depthOfField.focusDistance.value = 10f;

            curentHealth = GetHealth();
            vignette.intensity.value = Mathf.Abs(((float)curentHealth*.01f)-1);


            itemInHand = new Item[Hand.transform.childCount];
            for (int i = 0; i < itemInHand.Length; i++)
            {
                itemInHand[i] = Hand.transform.GetChild(i).GetComponent<Item>();
            }
        }


        private void OnEnable()
        {
            GameManager.Instance.onGameStateChange +=OnGameStateChange;
            onPlayerStateChange += OnPlayerStateChange;
            onHandChange += OnHandChange;
            onHealthChange += OnHealthChange;
        }


        private void OnDisable()
        {
            GameManager.Instance.onGameStateChange -= OnGameStateChange;
            onHandChange -= OnHandChange;
            onHealthChange -= OnHealthChange;
            onPlayerStateChange -= OnPlayerStateChange;
        }

        private new void OnPlayerStateChange(PLAYERSTATE newstate)
        {
            switch (newstate)
            {
                case PLAYERSTATE.NORMAL:
                    break;
                case PLAYERSTATE.HITTED:
                    StartCoroutine(GetHit());
                    break;
                case PLAYERSTATE.EAT:
                    StartCoroutine( UseItemAnimation(""));
                    break;
                case PLAYERSTATE.DRINK:
                    // start korotin use item
                    break;
                case PLAYERSTATE.DEATH:
                    StartCoroutine(Death());
                    break;
                default:
                    break;
            }
        }
        private void OnGameStateChange(GameState state)
        {
            switch (state)
            {
                case GameState.GAME:
                    GetComponent<FirstPersonController>().enabled = true;
                    depthOfField.focusDistance.value = 10f;
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
                case GameState.WIN:
                    break;
                case GameState.LOSE:
                    break;
                case GameState.GAMEPAUSED:
                    GetComponent<FirstPersonController>().enabled = false;
                    depthOfField.focusDistance.value = 0.1f;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case GameState.GAMERESUME:
                    GetComponent<FirstPersonController>().enabled = true;
                    depthOfField.focusDistance.value = 10f;
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
                case GameState.OPENINVENTORY:
                    depthOfField.focusDistance.value = 0.1f;
                    break;
                case GameState.OPENGUIDE:
                    GetComponent<FirstPersonController>().enabled = false;
                    depthOfField.focusDistance.value = 0.1f;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                default:
                    break;
            }
        }

        private new void OnHealthChange(int health)
        {
            if (health < curentHealth)
                SetState(PLAYERSTATE.HITTED);

            curentHealth = health;
            vignette.intensity.value = Mathf.Abs(((float)curentHealth * .01f) - 1);
        }
        private new void OnHandChange(Item _itemInHand)
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
        private IEnumerator GetHit()
        {
            SetState(PLAYERSTATE.NORMAL);
            yield return null;
        }


        private IEnumerator Death()
        {
            throw new NotImplementedException();
        }

        private IEnumerator UseItemAnimation(string name)
        {
            SetState(PLAYERSTATE.NORMAL);
            yield return null;
        }

    }

}
