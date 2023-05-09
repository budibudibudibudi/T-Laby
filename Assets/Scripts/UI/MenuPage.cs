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
            CanvasManager canvasManager = GetComponentInParent<CanvasManager>();
            settingPanel.onClick.AddListener(() => canvasManager.SetPage(PageName.SETTINGPAGE));
            exitBTN.onClick.AddListener(() => {
                canvasManager.SetPage(PageName.CONFIRMPAGE);
                ConfirmPage.Instance.Confirm(ConfirmType.EXITTYPE);
                });
        }
    }

}
