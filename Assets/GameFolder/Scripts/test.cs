using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UWAK.GAME.PLAYER;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class test : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    bool a;
    private void Update()
    {
        if (a)
            Debug.Log("exit");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        a = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        a = false;
    }
}
