using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField] private int networkSceneId, splitscreenSceneId;

    public void LoadNetWorkGame()
    {
        SceneManager.LoadScene(networkSceneId);
    }

    public void LoadSplitScreenGame()
    {
        SceneManager.LoadScene(splitscreenSceneId);
    }
}
