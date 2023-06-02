using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UWAK.SAVELOAD;

namespace UWAK.UI
{
    public class GuidePage : Page
    {
        [SerializeField] Button closeBTN;

        private void Start()
        {
            closeBTN.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.GAMERESUME)); 
        }
    }

}