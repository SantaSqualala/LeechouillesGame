using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        FindObjectOfType<GameManager>().UnPauseGame();
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
