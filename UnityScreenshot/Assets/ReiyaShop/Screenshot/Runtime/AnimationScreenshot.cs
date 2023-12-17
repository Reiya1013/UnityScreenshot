//******************************************************
// UnityScreenshot
// © 2023 Reiya1013
// MIT Licence
//******************************************************
using UnityEngine;

public class AnimationScreenshot : MonoBehaviour
{
    [SerializeField]
    private Animator _avatar;

    [SerializeField]
    private AnimationClip _clip;

    [SerializeField]
    private AnimationClip _faceclip;

    [SerializeField]
    private float _displayTime;

    [SerializeField]
    private bool _isFixCameraSetting;

    [SerializeField]
    private bool _isAlpha;

    [SerializeField]
    private string _saveFolder;

}