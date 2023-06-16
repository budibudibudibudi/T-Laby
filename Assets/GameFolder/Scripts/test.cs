using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UWAK.GAME.PLAYER;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class test : MonoBehaviour
{
    private IEnumerator Start()
    {
        var go = new GameObject("", typeof(test2));
        print(go == null);
        Destroy(go);
        yield return new WaitForEndOfFrame();
        print(go == null);
    }
}
public class test2 : MonoBehaviour
{

}
