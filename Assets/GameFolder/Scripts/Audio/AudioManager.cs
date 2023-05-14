using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UWAK.AUDIO
{
    public class AudioManager : MonoBehaviour
    {
        #region SINGLETON
        public static AudioManager Instance;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
        #endregion
        [SerializeField] AudioClip hittedDoor;
        public AudioClip GetHittedDoor() { return hittedDoor; }

    }


}