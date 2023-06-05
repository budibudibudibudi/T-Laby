using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShortcutButton : MonoBehaviour
{
    [ToolbarRight]
    public static void OpenSceneFolder()
    {
        AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<DefaultAsset>("Assets/GameFolder/Scenes"));
    }
    [ToolbarRight]
    public static void OpenScriptFolder()
    {
        AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<DefaultAsset>("Assets/GameFolder/Scripts"));
    }
}
