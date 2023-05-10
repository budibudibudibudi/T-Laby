using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UWAK.UI;

namespace UWAK.UI
{
    public class GameoverPage : Page
    {
        [SerializeField] Button menuBTN, restartBTN;
        [SerializeField] TMPro.TMP_Text cooldownText;
        private float Cooldown;
        void Start()
        {
            menuBTN.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.MENUPAGE));
            restartBTN.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
        }

        // Update is called once per frame
        void Update()
        {
            Cooldown -= 1 * Time.deltaTime;
            cooldownText.text = Cooldown.ToString("0");
        }
    }
}