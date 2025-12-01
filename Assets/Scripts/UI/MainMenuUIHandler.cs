using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIHandler : MonoBehaviour
{
    public void GoToGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
            Application.Quit();
    }
}
