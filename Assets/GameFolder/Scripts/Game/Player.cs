using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UWAK.ITEM;

namespace UWAK.GAME
{
    public class Player : MonoBehaviour
    {
        [SerializeField] GameObject Camera;
        [SerializeField] GameObject interactUI;
        [SerializeField] TMPro.TMP_Text capsuleHealthText;

        private StarterAssetsInputs _input;

        DepthOfField depthOfField;

        [SerializeField] private int capsuleHealth = 3;
        private void Start()
        {
            Volume volumeCamera = Camera.GetComponent<Volume>();
            _input = GetComponent<StarterAssetsInputs>();
            volumeCamera.profile.TryGet(out depthOfField);
            depthOfField.mode.overrideState = true;
            depthOfField.mode.value = DepthOfFieldMode.Bokeh;
            depthOfField.focusDistance.value = 10f;
        }
        private void OnEnable()
        {
            GameManager.Instance.onGameStateChange +=OnGameStateChange;
        }
        private void OnDisable()
        {
            GameManager.Instance.onGameStateChange -= OnGameStateChange;
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
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2f))
            {
                if (hit.collider.tag == "Pintu")
                {
                    interactUI.SetActive(true);
                    if (_input.interact)
                    {
                        Door pintu = hit.collider.gameObject.GetComponent<Door>();
                        if(!pintu.GetDoorState())
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

            if(_input.useItem)
            {
                UseHeal(-1);
                _input.useItem = false;
            }
        }

        private void UseHeal(int amount)
        {
            Mathf.Clamp(capsuleHealth + amount, 0, 3);

        }
    }

}
