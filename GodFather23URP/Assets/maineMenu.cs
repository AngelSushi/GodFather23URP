using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class maineMenu : MonoBehaviour
{
    public string levelToLoad;

    public GameObject settingsWindow;

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void SettingsButon()
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
