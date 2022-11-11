//******************************************************
// UnityScreenshot
// © 2022 Reiya1013
// MIT Licence
//******************************************************
using System;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Screenshot : MonoBehaviour
{

    [MenuItem("Edit/Screenshot/WithAlpha",priority =1)]
    private static void IsWithAlpha()
    {
        //反転条件を取得してセットする
        Menu.SetChecked("Edit/Screenshot/WithAlpha", !Menu.GetChecked("Edit/Screenshot/WithAlpha"));
    }

    /// <summary>
    /// スクリーンショットを取る(Assetsフォルダに自動生成)
    /// </summary>
    /// <remarks>
    /// Edit > Screenshot > Save AssetsFolder に追加。
    /// HotKeyは Ctrl + Shift + F12。
    /// </remarks>
    [MenuItem("Edit/Screenshot/Save AssetsFolder #%F10", priority = 20)]
    private static void ScreenshotSaveAssetsFolder()
    {
        // 現在時刻からファイル名を決定
        var path = System.DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".png";

        Debug.Log($"isWithAlpha:{Menu.GetChecked("Edit/Screenshot/WithAlpha")}");

        if (Menu.GetChecked("Edit/Screenshot/WithAlpha"))
        {
            PlayerPrefs.SetInt("Screenshot_isCapture", 1);
            PlayerPrefs.SetString("Screenshot_path", path);
        }
        else
        {
            SaveScreenshot(path);
        }
    }

    /// <summary>
    /// スクリーンショットを取る(Assetsフォルダに自動生成)
    /// </summary>
    /// <remarks>
    /// Edit > Screenshot > Save AssetsFolder に追加。
    /// HotKeyは Ctrl + Shift + F12。
    /// </remarks>
    [MenuItem("Edit/Screenshot/Save SelectFolder #%F11", priority = 21)]
    private static void ScreenshotSaveSelectFolder()
    {
        var path = EditorUtility.SaveFilePanel("Save Screenshot", UnityEngine.Application.dataPath, System.DateTime.Now.ToString("yyyyMMdd-HHmmss"), "png");
        if (string.IsNullOrEmpty(path))
            return;

        Debug.Log($"isWithAlpha:{Menu.GetChecked("Edit/Screenshot/WithAlpha")}");

        if (Menu.GetChecked("Edit/Screenshot/WithAlpha"))
        {
            PlayerPrefs.SetInt("Screenshot_isCapture", 1);
            PlayerPrefs.SetString("Screenshot_path", path);
        }
        else
        {
            SaveScreenshot(path);
        }
    }

    /// <summary>
    /// GameViewの3倍解像度で保存する
    /// </summary>
    /// <param name="path"></param>
    private static void SaveScreenshot(string path)
    {
        // キャプチャを撮る
        ScreenCapture.CaptureScreenshot(path, 3);

        // GameViewを取得してくる
        var assembly = typeof(EditorWindow).Assembly;
        var type = assembly.GetType("UnityEditor.GameView");
        var gameview = EditorWindow.GetWindow(type);
        // GameViewを再描画
        gameview.Repaint();

        Debug.Log("ScreenShot: " + path);

    }

}
