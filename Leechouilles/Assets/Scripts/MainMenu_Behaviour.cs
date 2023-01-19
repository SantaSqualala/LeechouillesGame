using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Behaviour : MonoBehaviour
{
    public int splitSceneID, networkSceneID;

    public void LoadSplitScreenGame()
    {
        SceneManager.LoadScene(splitSceneID);
    }

    public void LoadNetworkGame()
    {
        SceneManager.LoadScene(networkSceneID);
    }
}
