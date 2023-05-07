using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UWAK.UI;
namespace UWAK.UI
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] protected Page[] allPages;

        public virtual void SetPage(PageName pageName)
        {
            foreach (var page in allPages)
            {
                page.gameObject.SetActive(false);
            }
            Page currentPage = Array.Find(allPages, p => p.pageName == pageName);
            if (currentPage != null)
            {
                currentPage.gameObject.SetActive(true);
            }
        }
    }

}
