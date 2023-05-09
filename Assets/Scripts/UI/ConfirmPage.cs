using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UWAK.UI;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UWAK.UI
{
    public class ConfirmPage : Page
    {
        public static ConfirmPage Instance;
        [SerializeField] Button yesBTN, noBTN;
        [SerializeField] TMPro.TMP_Text descriptionText;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
        public void Confirm(ConfirmType type)
        {
            CanvasManager canvasManager = GetComponentInParent<CanvasManager>();
            switch (type)
            {
                case ConfirmType.NEWGAMETYPE:
                    break;
                case ConfirmType.EXITTYPE:
                    descriptionText.text = "Are you sure want to exit?";
                    yesBTN.onClick.AddListener(() => Application.Quit());
                    noBTN.onClick.AddListener(() => canvasManager.SetPage(PageName.MENUPAGE));
                    break;
                default:
                    break;
            }
        }
    }
}
[System.Serializable]
public enum ConfirmType
{
    NEWGAMETYPE,
    EXITTYPE
}
