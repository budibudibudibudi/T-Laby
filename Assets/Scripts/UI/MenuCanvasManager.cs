using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UWAK.UI;
namespace UWAK.UI
{
    public class MenuCanvasManager : CanvasManager
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
                case GameState.MENUPAGE:
                    if (SceneManager.GetActiveScene().name == "MainMenu")
                        SetPage(PageName.MENUPAGE);
                    else
                        ChangeScene("MainMenu");
                    break;
                case GameState.SETTINGPAGE:
                    SetPage(PageName.SETTINGPAGE);
                    break;
                case GameState.CREDITPAGE:
                    SetPage(PageName.CREDITPAGE);
                    break;
                case GameState.CONFIRMPAGE:
                    SetPage(PageName.CONFIRMPAGE);
                    break;
                case GameState.GAMEPAUSED:
                    SetPage(PageName.PAUSEDPAGE);
                    break;
                case GameState.GAMERESUME:
                    SetPage(null);
                    break;
                case GameState.GAME:
                    ChangeScene("Game");
                    break;
                default:
                    break;
            }
        }
    }
}
