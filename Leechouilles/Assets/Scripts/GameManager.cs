using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private bool hasHunter = false;
    private List<LobbyPlayerBehaviour> players = new List<LobbyPlayerBehaviour>();
    
    [SerializeField] private GameObject hunterPrefab;
    [SerializeField] private GameObject alienPrefab;

    private void Update()
    {

    }

    private void LaunchGame()
    {

    }

    public void PlayerJoin(LobbyPlayerBehaviour player)
    {
        if(hasHunter)
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
}