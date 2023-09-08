using UnityEngine;
using UnityEngine.SceneManagement;


public class ManaUi : MonoBehaviour
{
    public void MainMenuButton()
    {
        SceneManager.LoadScene("menu");
    }

    public void QuitterButton()
    {
        Application.Quit();
    }

    public void restart ()
    {
        SceneManager.LoadScene("Fix");
    }
}
