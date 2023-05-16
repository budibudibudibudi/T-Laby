using StarterAssets;
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UWAK.ITEM;

namespace UWAK.GAME.PLAYER
{
    public class Player : MonoBehaviour
    {
        [SerializeField] GameObject Camera;
        [SerializeField] GameObject interactUI;

        [SerializeField] private float distance;

        private StarterAssetsInputs _input;

        DepthOfField depthOfField;

        private int curentHealth;
        private void Start()
        {
            Volume volumeCamera = Camera.GetComponent<Volume>();
            _input = GetComponent<StarterAssetsInputs>();
            curentHealth = Character.Instance.GetHealth();

            volumeCamera.profile.TryGet(out depthOfField);
            depthOfField.mode.overrideState = true;
            depthOfField.mode.value = DepthOfFieldMode.Bokeh;
            depthOfField.focusDistance.value = 10f;
        }
        private void OnEnable()
        {
            GameManager.Instance.onGameStateChange +=OnGameStateChange;
            Character.Instance.onHealUsed += OnHealUsed;
            Character.Instance.onHealthChange += OnHealthChange;
        }
        private void OnDisable()
        {
            GameManager.Instance.onGameStateChange -= OnGameStateChange;
            Character.Instance.onHealUsed -= OnHealUsed;
            Character.Instance.onHealthChange -= OnHealthChange;
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
                    break;
                case GameState.GAMERESUME:
                    depthOfField.focusDistance.value = 10f;
                    break;
                default:
                    break;
            }
        }
        private void Update()
        {
            RaycastHit hit;
            if (!Physics.Raycast(Camera.transform.position, Camera.transform.TransformDirection(Vector3.forward), out hit, distance))
            {
                interactUI.SetActive(false);
            }
            else
            {
                Debug.DrawLine(Camera.transform.position, hit.point, Color.green);
                if (hit.collider.tag == "Pintu")
                {
                    interactUI.SetActive(true);
                    if (_input.interact)
                    {
                        Door pintu = hit.collider.gameObject.GetComponent<Door>();
                        if (!pintu.GetDoorState())
                        {
                            //StartCoroutine(pintu.SetDoorState(true));
                            pintu.Use(true);
                        }
                        else
                        {
                            //StartCoroutine(pintu.SetDoorState(false));
                            pintu.Use(false);
                        }
                    }
                }
                else
                {
                    interactUI.SetActive(false);
                }
            }

            if (_input.useItem)
            {
                Character.Instance.UseHeal(1);
                _input.useItem = false;
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

        private void OnHealUsed(int amount)
        {
            //animasi player pake heal
        }
        public void Death()
        {

        }
    }

}
