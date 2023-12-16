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

    //[MenuItem("Edit/Screenshot/WithAlpha",priority =1)]
    //private static void IsWithAlpha()
    //{
    //    //反転条件を取得してセットする
    //    Menu.SetChecked("Edit/Screenshot/WithAlpha", !Menu.GetChecked("Edit/Screenshot/WithAlpha"));
    //}

    ///// <summary>
    ///// スクリーンショットを取る(Assetsフォルダに自動生成)
    ///// </summary>
    ///// <remarks>
    ///// Edit > Screenshot > Save AssetsFolder に追加。
    ///// HotKeyは Ctrl + Shift + F10。
    ///// </remarks>
    //[MenuItem("Edit/Screenshot/Save AssetsFolder #%F10", priority = 20)]
    //private static void ScreenshotSaveAssetsFolder()
    //{
    //    // 現在時刻からファイル名を決定
    //    var path = System.DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".png";

    //    Debug.Log($"isWithAlpha:{Menu.GetChecked("Edit/Screenshot/WithAlpha")}");

    //    if (Menu.GetChecked("Edit/Screenshot/WithAlpha"))
    //    {
    //        AddAndSelect8K();
    //        PlayerPrefs.SetInt("Screenshot_isCapture", 1);
    //        PlayerPrefs.SetString("Screenshot_path", path);
    //    }
    //    else
    //    {
    //        SaveScreenshot(path);
    //    }
    //}

    ///// <summary>
    ///// スクリーンショットを取る(Assetsフォルダに自動生成)
    ///// </summary>
    ///// <remarks>
    ///// Edit > Screenshot > Save AssetsFolder に追加。
    ///// HotKeyは Ctrl + Shift + F11。
    ///// </remarks>
    //[MenuItem("Edit/Screenshot/Save SelectFolder #%F11", priority = 21)]
    //private static void ScreenshotSaveSelectFolder()
    //{
    //    var path = EditorUtility.SaveFilePanel("Save Screenshot", UnityEngine.Application.dataPath, System.DateTime.Now.ToString("yyyyMMdd-HHmmss"), "png");
    //    if (string.IsNullOrEmpty(path))
    //        return;

    //    Debug.Log($"isWithAlpha:{Menu.GetChecked("Edit/Screenshot/WithAlpha")}");

    //    if (Menu.GetChecked("Edit/Screenshot/WithAlpha"))
    //    {
    //        AddAndSelect8K();
    //        PlayerPrefs.SetInt("Screenshot_isCapture", 1);
    //        PlayerPrefs.SetString("Screenshot_path", path);
    //    }
    //    else
    //    {
    //        SaveScreenshot(path);
    //    }
    //}

    ///// <summary>
    ///// GameViewの3倍解像度で保存する
    ///// </summary>
    ///// <param name="path"></param>
    //private static void SaveScreenshot(string path)
    //{
    //    // キャプチャを撮る
    //    ScreenCapture.CaptureScreenshot(path, 3);

    //    // GameViewを取得してくる
    //    var assembly = typeof(EditorWindow).Assembly;
    //    var type = assembly.GetType("UnityEditor.GameView");
    //    var gameview = EditorWindow.GetWindow(type);
    //    // GameViewを再描画
    //    gameview.Repaint();

    //    Debug.Log("ScreenShot: " + path);

    //}

    //private static void AddAndSelect8K()
    //{
    //    var groupType = GameViewSizeGroupType.Standalone;

    //    var size = new GameViewSizeHelper.GameViewSize
    //    {
    //        type = GameViewSizeHelper.GameViewSizeType.FixedResolution,
    //        width = 7680,
    //        height = 3420,
    //        baseText = "8K"
    //    };
    //    // 存在する場合 true
    //    var isExist = GameViewSizeHelper.Contains(groupType, size);

    //    if (!isExist)
    //    {
    //        // 追加
    //        GameViewSizeHelper.AddCustomSize(groupType, size);
    //    }

    //    // 変更
    //    GameViewSizeHelper.ChangeGameViewSize(groupType, size);
    //}
}
