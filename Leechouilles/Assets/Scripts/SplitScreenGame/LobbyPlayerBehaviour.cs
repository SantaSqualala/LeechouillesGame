using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class LobbyPlayerBehaviour : MonoBehaviour
{
    private GameManager gameManager;
    private InputHandler inputHandler;
    private bool isReady = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        inputHandler = GetComponent<InputHandler>();
        FindObjectOfType<SpawnManager>().Spawn(this.gameObject);
        OnPlayerJoin();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if(inputHandler.inputActionR())
        {
            isReady = true;
        }
    }

    private void OnPlayerJoin()
    {
        gameManager.PlayerJoin(this);
    }

    public bool isPlayerReady()
    {
        return isReady;
    }
}
