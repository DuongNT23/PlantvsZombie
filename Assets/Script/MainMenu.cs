using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnClickStart()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void OnClickContinue()
    {

    }

    public void OnClickSettings()
    {

    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
