using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public Transform[] spawns;
    public GameObject alienPrefab;
    bool play = false;
    private bool hasHunter = false;

    private void StartPlayers()
    {
        int i = 0;

        foreach (GameObject player in players)
        {
            player.GetComponent<InputHandler>().StartPlayer();
            player.transform.position = spawns[i].transform.position;
            player.transform.rotation = spawns[i].transform.rotation;
            i++;
        }
    }

    public void AddPlayer()
    {
        players.Clear();

        hasHunter = true;
        
        if(hasHunter)
        {
            GetComponent<PlayerInputManager>().playerPrefab = alienPrefab;
        }

        foreach (InputHandler player in FindObjectsOfType<InputHandler>())
        {
            players.Add(player.gameObject);
        }
    }

    private void Update()
    {
        Debug.Log(players.Count);

        if (players.Count >= 2 && Input.GetKeyDown(KeyCode.Space) && !play)
        {
            play = true;
            StartPlayers();
        }
    }
}
