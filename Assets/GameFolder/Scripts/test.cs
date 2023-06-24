using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UWAK.GAME.PLAYER;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class test : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
    }
}
