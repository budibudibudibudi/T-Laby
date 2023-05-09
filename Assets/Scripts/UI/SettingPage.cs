using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace UWAK.UI
{
    public class SettingPage : Page
    {
        [SerializeField] Button backBTN, creditBTN;
        [SerializeField] TMP_Dropdown ResolutionDD;
        [SerializeField] Toggle FullScreenTOOGLE;
        [SerializeField] Slider volumeSlider;
        void Start()
        {
            CanvasManager canvasManager = GetComponentInParent<CanvasManager>();
            backBTN.onClick.AddListener(() => canvasManager.SetPage(PageName.MENUPAGE));
            creditBTN.onClick.AddListener(() => canvasManager.SetPage(PageName.CREDITPAGE));

            ResolutionDD.onValueChanged.AddListener(OnResolutionChange);

            FullScreenTOOGLE.onValueChanged.AddListener(OnFullscreenToogleChange);

            volumeSlider.onValueChanged.AddListener(OnVolumeChange);

            Volume();
            IsiResolutionDropdown();
        }

        private void Volume()
        {
            if (PlayerPrefs.HasKey("Volume"))
                volumeSlider.value = PlayerPrefs.GetFloat("Volume");
            else
                volumeSlider.value = 1;
        }

        private void OnVolumeChange(float _volume)
        {
            AudioSource[] listAllAudio = FindObjectsOfType<AudioSource>();
            for (int i = 0; i < listAllAudio.Length; i++)
            {
                listAllAudio[i].volume = _volume;
            }
            PlayerPrefs.SetFloat("Volume", _volume);
        }

        private void OnFullscreenToogleChange(bool isOn)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, isOn);
        }

        private void IsiResolutionDropdown()
        {
            List<string> options = new List<string>();
            options.Add("1920 X 1080");//INDEX 0
            options.Add("1280 X 720");//INDEX 1
            options.Add("800 X 600");//INDEX 2
            ResolutionDD.AddOptions(options);
        }

        private void OnResolutionChange(int index)
        {
            if(index == 0)
            {
                Screen.SetResolution(1920, 1080, true);
            }
            else if(index == 1)
            {
                Screen.SetResolution(1280, 720, true);
            }
            else if(index == 2)
            {
                Screen.SetResolution(800, 600, true);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
