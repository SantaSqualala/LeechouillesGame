using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreenGameManager : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public bool play = false;

    private void StartPlayers()
    {
        foreach(GameObject player in players)
        {
            player.GetComponent<SplitScreenInputManager>().StartPlayer();
        }
    }

    public void AddPlayer()
    {
        players.Clear();

        foreach(SplitScreenInputManager manager in FindObjectsOfType<SplitScreenInputManager>())
        {
            players.Add(manager.gameObject);
        }
    }

    private void Update()
    {
        if(players.Count >= 2 && Input.GetKeyDown(KeyCode.Space))
        {
            play = true;
            StartPlayers();
        }
    }
}
