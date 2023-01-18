using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Vector2 movementInput;
    private Vector2 lookInput;
    private bool fire;
    private bool scan;
    private bool jump;
    private bool play = false;

    public void StartPlayer()
    {
        play = true;
    }

    public void StopPlayer()
    {
        play = false;
    }

    #region Set Input
    public void InputMovement(InputAction.CallbackContext context)
    {
        if(play)
            movementInput = context.ReadValue<Vector2>();
        else
            movementInput = Vector2.zero;
    }

    public void InputLook(InputAction.CallbackContext context)
    {
        if (play)
            lookInput = context.ReadValue<Vector2>();
        else
            movementInput = Vector2.zero;
    }

    public void InputJump(InputAction.CallbackContext context)
    {
        if (play)
            jump = context.ReadValueAsButton();
        else
            jump = false;
    }

    public void InputFire(InputAction.CallbackContext context)
    {
        if (play)
            fire = context.ReadValueAsButton();
        else
            fire = false;

    }

    public void InputScan(InputAction.CallbackContext context)
    {
        if (play)
            scan = context.ReadValueAsButton();
        else
            scan = false;
    }
    #endregion

    
    #region Get Input
    // Return the movement vector2 input
    public Vector2 inputMovement()
    {
        return movementInput;
    }

    // Return the look vector2 input
    public Vector2 inputLook()
    {
        return lookInput;
    }

    // Return true if left action key is pressed
    public bool inputFire()
    {
        return fire;
    }

    // Return true if right action key is pressed
    public bool inputScan()
    {
        return scan;
    }

    // Return true if jump key is pressed
    public bool inputJump()
    {
        return jump;
    }
    #endregion
}
