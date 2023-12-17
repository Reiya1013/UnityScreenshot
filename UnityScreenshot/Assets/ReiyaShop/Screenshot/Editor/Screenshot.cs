//******************************************************
// UnityScreenshot
// © 2022-2023 Reiya1013
// MIT Licence
//******************************************************
using Kyusyukeigo.Helper;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    [MenuItem("ReiyaShop/Add screenshot Inspector")]
    private static void AddScreenshotObject()
    {
        string guid = "dbfe01bf41d3ec54cbaaffb4db9341f8";
        var path = AssetDatabase.GUIDToAssetPath(guid);
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        PrefabUtility.InstantiatePrefab(obj);
    }
}
