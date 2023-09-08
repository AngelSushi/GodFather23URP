using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class maineMenu : MonoBehaviour
{
    public string levelToLoad;

    public GameObject settingsWindow;
    public GameObject scoresBoardWindow;

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

    public void OpenScoresBoard()
    {
        scoresBoardWindow.SetActive(true);
    }

    public void CloseScoresBoard()
    {
        scoresBoardWindow.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
