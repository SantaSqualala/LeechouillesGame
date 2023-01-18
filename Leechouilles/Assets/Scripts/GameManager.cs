using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static NetPlayerBehaviour;

public class GameManager : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    private SpawnManager spawnManager;

    private bool hasHunter = false;
    private List<LobbyPlayerBehaviour> players = new List<LobbyPlayerBehaviour>();
    private List<GameObject> playerList = new List<GameObject>();

    [SerializeField] private GameObject hunterPrefab;
    [SerializeField] private GameObject alienPrefab;

    private void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        spawnManager = GetComponent<SpawnManager>();
        playerInputManager.playerPrefab = hunterPrefab;
    }

    // Depreceated
    public void PlayerJoin(LobbyPlayerBehaviour player)
    {
        if (hasHunter)
        {
            Instantiate(alienPrefab, player.transform);
        }
        else
        {
            Instantiate(hunterPrefab, player.transform);
            hasHunter = true;
        }

        players.Add(player);
    }

    // triggers on player joining game
    public void OnPlayerJoined()
    {
        hasHunter = true;

        if (hasHunter)
        {
            playerInputManager.playerPrefab = alienPrefab;
        }

        foreach (SplitScreenInputHandler input in FindObjectsOfType<SplitScreenInputHandler>())
        {
            Debug.Log(input.GetComponent<PlayerInput>().name);

            if(!playerList.Contains(input.gameObject))
            {
                playerList.Add(input.gameObject);
            }

            spawnManager.Spawn(input.gameObject);
        }
    }

    public void PauseGame()
    {

    }

    public void UnPauseGame()
    {

    }
}