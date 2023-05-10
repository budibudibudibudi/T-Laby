using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UWAK.UI;

namespace UWAK.UI
{
    public class CreditPage : Page
    {
        [SerializeField] Button skipBTN;
        private void Start()
        {
            skipBTN.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.MENUPAGE));
        }
        public void EndAnimation()
        {
            GameManager.Instance.ChangeState(GameState.MENUPAGE);
        }
        public void ShowSkipButton()
        {
            skipBTN.gameObject.SetActive(true);
        }
    }

}
