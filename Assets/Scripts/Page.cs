using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UWAK.UI
{
    public class Page : MonoBehaviour
    {
        [SerializeField] public PageName pageName;

        protected virtual void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}