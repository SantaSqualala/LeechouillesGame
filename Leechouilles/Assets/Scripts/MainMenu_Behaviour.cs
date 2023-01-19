using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Behaviour : MonoBehaviour
{
    public int splitSceneID, networkSceneID;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void LoadSplitScreenGame()
    {
        SceneManager.LoadScene(splitSceneID);
    }

    public void LoadNetworkGame()
    {
        SceneManager.LoadScene(networkSceneID);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
