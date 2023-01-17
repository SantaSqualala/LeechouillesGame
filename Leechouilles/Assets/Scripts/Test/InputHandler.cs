using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Vector2 movementInput;
    private Vector2 lookInput;
    private bool actionL;
    private bool actionR;
    private bool jump;
    private bool isGamepad;

    // Fixed Update is called once every physics call
    private void FixedUpdate()
    {
        var gamepad = Gamepad.current;

        if (gamepad != null && gamepad.enabled)
        {
            isGamepad = true;
            //Debug.Log("gamepad connected");
        }

        //Debug.Log(gamepad);
    }

    #region Set Input
    public void InputMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>().normalized;
    }

    public void InputLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>().normalized;
    }

    public void InputActionL(InputAction.CallbackContext context)
    {
        actionL = context.ReadValueAsButton();
    }

    public void InputActionR(InputAction.CallbackContext context)
    {
        actionR = context.ReadValueAsButton();
    }

    public void InputActionJump(InputAction.CallbackContext context)
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
    public bool inputActionL()
    {
        return actionL;
    }

    // Return true if right action key is pressed
    public bool inputActionR()
    {
        return actionR;
    }

    // Return true if jump key is pressed
    public bool inputJump()
    {
        return jump;
    }

    public bool isGamepadUsed()
    {
        return isGamepad;
    }
    #endregion
}
