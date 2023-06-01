using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[InitializeOnLoad]
public static class ToolbarButton 
{

    static ToolbarButton(){
        EditorApplication.delayCall -= OnInit;
        EditorApplication.delayCall += OnInit;

    }

    private static List<MethodInfo> _methods = new List<MethodInfo>();

    private static void OnInit(){
        var toolbarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");

        var objects = Resources.FindObjectsOfTypeAll(toolbarType);
        if(objects.Length == 0) return;
        var toolbar = objects[0] as ScriptableObject;
        var root = toolbar.GetType().GetField("m_Root", BindingFlags.Instance | BindingFlags.NonPublic)
            .GetValue(toolbar) as VisualElement;
        var toolbarKanan =  root.Q<VisualElement>("ToolbarZoneRightAlign");
        PopulateMethod();
        
        var container = new IMGUIContainer(DrawToolbar);
        toolbarKanan.Add(container);
    }

    private static void PopulateMethod(){
        var methods =  TypeCache.GetMethodsWithAttribute<ToolbarRightAttribute>();
        foreach (var method in methods) {
            if (method.IsStatic) {
                _methods.Add(method);
            }
        }
    }

    private static void DrawToolbar(){
        GUILayout.BeginHorizontal();
        
        foreach (var method in _methods) {
            if (GUILayout.Button(method.Name)) {
                method.Invoke(null, null);
            }    
        }

        GUILayout.EndHorizontal();
    }
}
