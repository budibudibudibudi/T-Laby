using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
                    if (SceneManager.GetActiveScene().name != "Game")
                        ChangeScene("Game");
                    break;
                case GameState.WIN:
                    SetPage(PageName.WINPAGE);
                    break;
                case GameState.LOSE:
                    SetPage(PageName.GAMEOVERPAGE);
                    break;
                case GameState.OPENINVENTORY:
                    SetPage(PageName.INVENTORYPAGE);
                    break;
                case GameState.OPENGUIDE:
                    Time.timeScale = 0;
                    SetPage(PageName.GUIDEPAGE);
                    break;
                default:
                    break;
            }
        }
    }
}
