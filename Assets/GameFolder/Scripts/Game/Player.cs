using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UWAK.GAME
{
    public class Player : MonoBehaviour
    {
        [SerializeField] GameObject Camera;
        [SerializeField] GameObject interactUI;

        DepthOfField depthOfField;
        private void Start()
        {
            Volume volumeCamera = Camera.GetComponent<Volume>();
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
                }
                else
                {
                    interactUI.SetActive(false);
                }
            }
        }
    }

}
