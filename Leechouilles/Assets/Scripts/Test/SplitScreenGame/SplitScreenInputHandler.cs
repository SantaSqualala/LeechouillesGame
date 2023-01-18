using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SplitScreenInputHandler : MonoBehaviour
{
    private Vector2 movementInput;
    private Vector2 lookInput;
    private bool fire;
    private bool scan;
    private bool jump;

    #region Register input
    public void RegisterMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>().normalized;
    }

    public void RegisterLookInput(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>().normalized;
    }

    public void RegisterFireInput(InputAction.CallbackContext context)
    {
        fire = context.ReadValueAsButton();
    }

    public void RegisterScanInput(InputAction.CallbackContext context)
    {
        scan = context.ReadValueAsButton();
    }

    public void RegisterJumpInput(InputAction.CallbackContext context)
    {
        jump = context.ReadValueAsButton();
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
