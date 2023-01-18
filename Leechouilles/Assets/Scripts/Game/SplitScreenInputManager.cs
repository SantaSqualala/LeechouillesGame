using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SplitScreenInputManager : MonoBehaviour
{
    Vector2 moveInput;
    Vector2 lookInput;
    bool jump;
    bool fire;
    bool scan;

    bool canMove;

    public void StartPlayer()
    {
        canMove = true;
    }

    public void DeathPlayer()
    {
        canMove = false;
    }
}
