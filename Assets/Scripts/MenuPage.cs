using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UWAK.UI;

namespace UWAK.UI
{
    public class MenuPage : Page
    {
        [SerializeField] Button playBTN, settingPanel, continueBTN, exitBTN;

        private void Start()
        {
            CanvasManager canvasManager = GetComponentInParent<CanvasManager>();
            playBTN.onClick.AddListener(() => ChangeScene("Game"));
        }
    }

}
