using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UWAK.UI;
using UWAK.SAVELOAD;

namespace UWAK.UI
{
    public class MenuPage : Page
    {
        [SerializeField] Button playBTN, settingPanel, continueBTN, exitBTN;

        private void Start()
        {
            SaveName saveName = SaveAndLoad.ReadFromJSON<SaveName>("SaveData");
            if(saveName == default)
            {
                playBTN.onClick.AddListener(()=> GameManager.Instance.ChangeState(GameState.GAME));
            }
            else
            {
                playBTN.onClick.AddListener(() =>
                {
                    GameManager.Instance.ChangeState(GameState.CONFIRMPAGE);
                    ConfirmPage.Instance.Confirm(ConfirmType.NEWGAMETYPE);
                });
                continueBTN.interactable = true;
            }
            settingPanel.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.SETTINGPAGE));
            exitBTN.onClick.AddListener(() => {
                GameManager.Instance.ChangeState(GameState.CONFIRMPAGE);
                ConfirmPage.Instance.Confirm(ConfirmType.EXITTYPE);
                });
        }
    }

}
