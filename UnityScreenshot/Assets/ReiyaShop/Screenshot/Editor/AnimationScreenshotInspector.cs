//******************************************************
// UnityScreenshot
// © 2023 Reiya1013
// MIT Licence
//******************************************************
using Kyusyukeigo.Helper;
using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using Transform = UnityEngine.Transform;


[CustomEditor(typeof(AnimationScreenshot))]
public class AnimationScreenshotInspector : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        // プロパティを最新の状態にする
        serializedObject.Update();

        if (!EditorApplication.isPlaying)
        {
            GUIPlaying();

        }
        else
        {
            GUINoPlaying();
        }
    }

    private void GUIPlaying()
    {
        // serializedObjectからSAnimationScreenshotのプロパティを取得
        SerializedProperty avatarProperty = serializedObject.FindProperty("_avatar");
        SerializedProperty clipProperty = serializedObject.FindProperty("_clip");
        SerializedProperty faceclipProperty = serializedObject.FindProperty("_faceclip");
        SerializedProperty displayTimeProperty = serializedObject.FindProperty("_displayTime");
        SerializedProperty isFixCameraSettingProperty = serializedObject.FindProperty("_isFixCameraSetting");
        SerializedProperty isAlphaProperty = serializedObject.FindProperty("_isAlpha");
        SerializedProperty saveFolderProperty = serializedObject.FindProperty("_saveFolder");

        // ラベル表示と共にAnimationScreenshot._avatarのプロパティを表示
        EditorGUILayout.LabelField("Avatarを選択してください");
        EditorGUILayout.PropertyField(avatarProperty);

        // スペースを空ける
        GUILayout.Space(EditorGUIUtility.singleLineHeight);

        // ラベル表示と共にAnimationScreenshot._clipのプロパティを表示
        EditorGUILayout.LabelField("再生するAnimationClipを選択してください");
        EditorGUILayout.PropertyField(clipProperty);

        var clip = (AnimationClip)clipProperty.objectReferenceValue;

        // ラベル表示と共にAnimationScreenshot._displayTimeのプロパティを表示
        EditorGUILayout.LabelField("再生位置を入力してください");
        displayTimeProperty.floatValue = EditorGUILayout.Slider("再生位置", displayTimeProperty.floatValue, 0, 1f);

        // ラベル表示と共にAnimationScreenshot._faceclipのプロパティを表示
        EditorGUILayout.LabelField("表情のAnimationClipを選択してください。未設定可");
        EditorGUILayout.PropertyField(faceclipProperty);


        // ラベル表示と共にAnimationScreenshot._isAlphaのプロパティを表示
        EditorGUILayout.LabelField("透過撮影するか選択してください");
        EditorGUILayout.PropertyField(isAlphaProperty);

        // ラベル表示と共にAnimationScreenshot._isFixCameraSettingのプロパティを表示
        EditorGUILayout.LabelField("カメラ位置を自動調整するか選択してください");
        EditorGUILayout.PropertyField(isFixCameraSettingProperty);

        // スペースを空ける
        GUILayout.Space(EditorGUIUtility.singleLineHeight);


        if (GUILayout.Button("保存先を選択。未入力の場合はAssetsフォルダに保存されます。"))
        {
            var path = EditorUtility.SaveFilePanel("Save Screenshot", Application.dataPath, System.DateTime.Now.ToString("yyyyMMdd-HHmmss"), "png");
            if (!string.IsNullOrEmpty(path))
                saveFolderProperty.stringValue = path;
        }
        EditorGUILayout.PropertyField(saveFolderProperty);

        // プロパティへの変更があった場合、それを反映する
        serializedObject.ApplyModifiedProperties();

        // スペースを空ける
        GUILayout.Space(EditorGUIUtility.singleLineHeight);
        if (GUILayout.Button("撮影開始"))
        {
            AddAndSelect8K();
            StartScene(true);
            if (isFixCameraSettingProperty.boolValue)
                SetCamera((Animator)avatarProperty.objectReferenceValue);
            if ((Animator)avatarProperty.objectReferenceValue != null && ((AnimationClip)clipProperty.objectReferenceValue != null || (AnimationClip)faceclipProperty.objectReferenceValue != null))
                SetAnimaton((Animator)avatarProperty.objectReferenceValue, (AnimationClip)clipProperty.objectReferenceValue, (AnimationClip)faceclipProperty.objectReferenceValue, displayTimeProperty.intValue);
        }
    }

    private void GUINoPlaying()
    {
        // serializedObjectからSAnimationScreenshotのプロパティを取得
        SerializedProperty avatarProperty = serializedObject.FindProperty("_avatar");
        SerializedProperty clipProperty = serializedObject.FindProperty("_clip");
        SerializedProperty faceclipProperty = serializedObject.FindProperty("_faceclip");
        SerializedProperty displayTimeProperty = serializedObject.FindProperty("_displayTime");
        SerializedProperty isFixCameraSettingProperty = serializedObject.FindProperty("_isFixCameraSetting");
        SerializedProperty isAlphaProperty = serializedObject.FindProperty("_isAlpha");
        SerializedProperty saveFolderProperty = serializedObject.FindProperty("_saveFolder");

        var clip = (AnimationClip)clipProperty.objectReferenceValue;

        // ラベル表示と共にAnimationScreenshot._displayTimeのプロパティを表示
        EditorGUILayout.LabelField("再生位置を入力してください");
        displayTimeProperty.floatValue = EditorGUILayout.Slider("再生位置", displayTimeProperty.floatValue, 0, 1f);

        // ラベル表示と共にAnimationScreenshot._isAlphaのプロパティを表示
        EditorGUILayout.LabelField("透過撮影するか選択してください");
        EditorGUILayout.PropertyField(isAlphaProperty);

        // スペースを空ける
        GUILayout.Space(EditorGUIUtility.singleLineHeight);


        if (GUILayout.Button("保存先を選択。未入力の場合はAssetsフォルダに保存されます。"))
        {
            var path = EditorUtility.SaveFilePanel("Save Screenshot", Application.dataPath, System.DateTime.Now.ToString("yyyyMMdd-HHmmss"), "png");
            if (!string.IsNullOrEmpty(path))
                saveFolderProperty.stringValue = path;
        }
        EditorGUILayout.PropertyField(saveFolderProperty);

        // プロパティへの変更があった場合、それを反映する
        serializedObject.ApplyModifiedProperties();

        // スペースを空ける
        GUILayout.Space(EditorGUIUtility.singleLineHeight);

        // アニメーションの再生位置を設定する
        if ((Animator)avatarProperty.objectReferenceValue != null)
            StartAnimation((Animator)avatarProperty.objectReferenceValue, displayTimeProperty.floatValue);

        if (GUILayout.Button("撮影"))
        {
            // 保存先未指定の場合、現在時刻からファイル名を決定
            if (string.IsNullOrEmpty(saveFolderProperty.stringValue))
            {
                saveFolderProperty.stringValue = "Assets/" + System.DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".png";
            }

            if (isAlphaProperty.boolValue)
            {
                PlayerPrefs.SetInt("Screenshot_isCapture", 1);
                PlayerPrefs.SetString("Screenshot_path", saveFolderProperty.stringValue);
            }
            else
            {
                SaveScreenshot(saveFolderProperty.stringValue);
            }

        }

        // スペースを空ける
        GUILayout.Space(EditorGUIUtility.singleLineHeight);

        if (GUILayout.Button("撮影停止"))
        {
            Animator animator = (Animator)avatarProperty.objectReferenceValue;
            animator = null;
            StartScene(false);
        }
    }

    private void SetCamera(Animator animator)
    {
        Camera.main.fieldOfView = 30;
        Camera.main.nearClipPlane = 0.01f;
        Transform transform = Camera.main.transform;
        Transform Head = animator.GetBoneTransform(HumanBodyBones.Head);
        Transform Spine = animator.GetBoneTransform(HumanBodyBones.Spine);

        transform.position = Spine.position + new Vector3(0 , -1 * Head.position.y * 0.1f, Head.position.y * 2.2f );
        // キャラクターの向きを進行方向に
        if (Spine.position != Vector3.zero)
        {
            transform.LookAt(Spine.position);
        }
    }

    private void StartScene(bool isStart)
    {
        UnityEditor.EditorApplication.isPlaying = isStart;
    }

    private void StartAnimation(Animator animator , float normartime)
    {
        var controller = (AnimatorController)animator.runtimeAnimatorController;
        var layer = controller.layers[0];
        animator.Play("State1", 0, normartime);
        animator.SetLayerWeight(controller.layers.Length - 1, 1f);
    }

    private void SetAnimaton(Animator animator,AnimationClip clip, AnimationClip faceclip, int startTime)
    {
        animator.runtimeAnimatorController = null;
        //AnimatorController animatorController = AnimatorController.CreateAnimatorControllerAtPath("Assets/test.controller"); //デバッグ保存用
        animator.runtimeAnimatorController = new AnimatorController();
        var controller = (AnimatorController)animator.runtimeAnimatorController;



        if (clip != null)
        {
            // Layer 追加
            controller.AddLayer("Pose Layer");
            var layer = controller.layers[controller.layers.Length - 1];
            var stateMachine = layer.stateMachine;

            // State 追加
            var state_no = stateMachine.AddState("State_no");
            var state = stateMachine.AddState("State1");
            state.speed = 0f;

            // Transition 追加
            stateMachine.AddAnyStateTransition(state_no);
            var transition = stateMachine.AddAnyStateTransition(state);

            // Animation 追加
            state.motion = clip;

            animator.SetLayerWeight(controller.layers.Length - 1, 0);
        }



        if (faceclip != null)
        {
            // Layer 追加
            controller.AddLayer("Face Layer");
            var layer = controller.layers[controller.layers.Length-1];
            layer.defaultWeight = 1;
            var stateMachine = layer.stateMachine;

            // State 追加
            var state = stateMachine.AddState("face");

            // Transition 追加
            var transition = stateMachine.AddAnyStateTransition(state);

            // Animation 追加
            state.motion = faceclip;
        }

        //// セーブ
        //AssetDatabase.SaveAssets(); //デバッグ保存用

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

        AssetDatabase.Refresh();
        Debug.Log("ScreenShot: " + path);
    }

    private static void AddAndSelect8K()
    {
        var groupType = GameViewSizeGroupType.Standalone;

        var size = new GameViewSizeHelper.GameViewSize
        {
            type = GameViewSizeHelper.GameViewSizeType.FixedResolution,
            width = 7680,
            height = 3420,
            baseText = "8K"
        };
        // 存在する場合 true
        var isExist = GameViewSizeHelper.Contains(groupType, size);

        if (!isExist)
        {
            // 追加
            GameViewSizeHelper.AddCustomSize(groupType, size);
        }

        // 変更
        GameViewSizeHelper.ChangeGameViewSize(groupType, size);
    }

}
