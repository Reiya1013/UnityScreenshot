//******************************************************
// UnityScreenshot
// © 2022 Reiya1013
// MIT Licence
//******************************************************
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 透過PNG保存
/// </summary>
public class AlphaScreenshot : MonoBehaviour
{
    
    /// <summary>
    /// パラメータを取得して透過保存を実行
    /// </summary>
    void Update()
    {
        var isCapture = PlayerPrefs.GetInt("Screenshot_isCapture", 0);

        if (isCapture == 1)
        {
            PlayerPrefs.SetInt("Screenshot_isCapture", 0);
            var path = PlayerPrefs.GetString("Screenshot_path", string.Empty);
            if (path != string.Empty)
            {
                StartCoroutine(SaceScreenshotWithAlpha(path));
            }
        }
    }

    /// <summary>
    /// 透過PNG保存
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private IEnumerator SaceScreenshotWithAlpha(string path)
    {
        //初期値取得
        var camera = Camera.main;
        var defaultClearFlags = camera.clearFlags;
        var defaultColor = camera.backgroundColor;

        //透過対応に変更
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = UnityEngine.Color.clear;
        camera.Render();

        yield return new WaitForEndOfFrame();

        var tex = ScreenCapture.CaptureScreenshotAsTexture();

        //透過PNGの受け皿作成
        var width = tex.width;
        var height = tex.height;
        var texAlpha = new Texture2D(width, height, TextureFormat.ARGB32, false);
        texAlpha.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        texAlpha.Apply();

        //透過PNGをByte変換
        var bytes = texAlpha.EncodeToPNG();
        Destroy(tex);
        //透過PNG保存
        File.WriteAllBytes(path, bytes);

        //もとの設定に戻す
        camera.clearFlags = defaultClearFlags;
        camera.backgroundColor = defaultColor;
        camera.Render();

        Debug.Log($"Compleat {path}");

    }
}
