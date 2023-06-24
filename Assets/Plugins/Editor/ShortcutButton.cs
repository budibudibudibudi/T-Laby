using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShortcutButton : MonoBehaviour
{
    [ToolbarRight]
    public static void OpenScene()
    {
        AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<DefaultAsset>("Assets/GameFolder/Scenes"));
    }
    [ToolbarRight]
    public static void OpenScript()
    {
        AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<DefaultAsset>("Assets/GameFolder/Scripts"));
    }
    [ToolbarRight]
    public static void OpenPrefab()
    {
        AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<DefaultAsset>("Assets/GameFolder/Prefabs"));
    }
}
