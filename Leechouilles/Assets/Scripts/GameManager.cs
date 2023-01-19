using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text announcer;
    public float gameTimer = 200f;

    public int hunterVictorySceneId = 3;
    public int alienVictorySceneId = 4;

    public List<GameObject> players = new List<GameObject>();
    public Transform[] spawns;
    public GameObject alienPrefab;

    bool play = false;
    bool hasHunter = false;
    int i = 0;

    private void StartPlayers()
    {
        announcer.text = "Time remaining" + gameTimer;
        announcer.fontSize = 35f;

        foreach (GameObject player in players)
        {
            player.GetComponent<InputHandler>().StartPlayer();
            player.transform.position = spawns[i].transform.position;
            player.transform.rotation = spawns[i].transform.rotation;

            if(player.GetComponent<CharacterController>() )
            {
                if(player.transform.position != spawns[i].transform.position)
                    player.GetComponent<CharacterController>().Move(Vector3.Lerp(player.transform.position, spawns[i].transform.position, 500f));
            }

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
        if (players.Count >= 2 && Input.GetKeyDown(KeyCode.Space) && !play)
        {
            play = true;
            StartPlayers();
        }

        if(play)
        {
            gameTimer -= Time.deltaTime;
            announcer.text = "Time remaining : " + (int)gameTimer;
        }

        if(gameTimer <= 0)
        {
            SceneManager.LoadScene(alienVictorySceneId);
        }
    }

    public void HunterWin()
    {
        SceneManager.LoadScene(hunterVictorySceneId);
    }
}
