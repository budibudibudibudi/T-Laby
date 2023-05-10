using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UWAK.UI;

namespace UWAK.UI
{
    public class WinPage : Page
    {
        [SerializeField] Button menuBTN, nextBTN;
        void Start()
        {
            menuBTN.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.MENUPAGE));
            nextBTN.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.CREDITPAGE));
        }
    }


}