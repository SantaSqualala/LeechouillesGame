
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFOVBehaviour : MonoBehaviour
{
    private PlayerInputManager plManager;

    // Start is called before the first frame update
    void Start()
    {
        plManager = FindObjectOfType<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(plManager.playerCount == 2)
        {
            GetComponent<Camera>().fieldOfView = 50f;
        }
        else
        {
            GetComponent<Camera>().fieldOfView = 75f;
        }
    }
}
