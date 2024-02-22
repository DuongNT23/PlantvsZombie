using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip _clickAudioClip;
    public void OnClickStart()
    {
        SoundManager.Instance.PlaySound(_clickAudioClip);
        SceneManager.LoadSceneAsync("LevelSelectScreen");
    }

    public void OnClickContinue()
    {
        SoundManager.Instance.PlaySound(_clickAudioClip);
    }

    public void OnClickSettings()
    {
        SoundManager.Instance.PlaySound(_clickAudioClip);
    }

    public void OnClickExit()
    {
        SoundManager.Instance.PlaySound(_clickAudioClip);
        Application.Quit();
    }
}
