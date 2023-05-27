using UnityEngine;
using UnityEngine.UI;

namespace UWAK.UI
{
    public class PausePage : Page
    {
        [SerializeField] Button resumeBTN, settingBTN, mainMenuBTN;
        // Start is called before the first frame update
        void Start()
        {
            resumeBTN.onClick.AddListener(() => {
                GameManager.Instance.ChangeState(GameState.GAMERESUME);
                Cursor.lockState = CursorLockMode.Locked;
            });
            settingBTN.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.SETTINGPAGE));
            mainMenuBTN.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                GameManager.Instance.ChangeState(GameState.MENUPAGE);
            });
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
