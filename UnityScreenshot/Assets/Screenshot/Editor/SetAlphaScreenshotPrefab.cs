//******************************************************
// UnityScreenshot
// © 2022 Reiya1013
// MIT Licence
//******************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SetAlphaScreenshotPrefab : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Start()
    {
        GameObject gameObject = new GameObject();
        gameObject.name = "AlphaScreenshot";
        gameObject.AddComponent<AlphaScreenshot>();
    }
}
