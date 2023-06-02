using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UWAK.SAVELOAD;

namespace UWAK.UI
{
    public class MenuPage : Page
    {
        [SerializeField] Button playBTN, settingPanel, continueBTN, exitBTN;

        private void Start()
        {
            SaveName saveName = SaveAndLoad.ReadFromJSON<SaveName>("SaveData");
            switch (saveName)
            {
                case SaveName.NULL:
                    playBTN.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.GAME));
                    break;
                case SaveName.LEVEL1:
                    playBTN.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.GAME));
                    break;
                case SaveName.LEVEL2:
                    playBTN.onClick.AddListener(() =>
                    {
                        GameManager.Instance.ChangeState(GameState.CONFIRMPAGE);
                        ConfirmPage.Instance.Confirm(ConfirmType.NEWGAMETYPE);
                    });
                    continueBTN.interactable = true;
                    break;
                case SaveName.LEVEL3:
                    playBTN.onClick.AddListener(() =>
                    {
                        GameManager.Instance.ChangeState(GameState.CONFIRMPAGE);
                        ConfirmPage.Instance.Confirm(ConfirmType.NEWGAMETYPE);
                    });
                    continueBTN.interactable = true;
                    break;
                default:
                    break;
            }
            settingPanel.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.SETTINGPAGE));
            exitBTN.onClick.AddListener(() => {
                GameManager.Instance.ChangeState(GameState.CONFIRMPAGE);
                ConfirmPage.Instance.Confirm(ConfirmType.EXITTYPE);
                });
        }

    }

}
