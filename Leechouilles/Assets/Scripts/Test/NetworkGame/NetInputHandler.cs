using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FishNet.Object;

public class NetInputHandler : NetworkBehaviour
{
    private Vector2 movementInput;
    private Vector2 lookInput;
    private bool actionL;
    private bool actionR;
    private bool isGamepad;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner)
        {
            Destroy(GetComponent<NetInputHandler>());
            return;
        }
    }

    // Fixed Update is called once every physics call
    private void FixedUpdate()
    {
        if (!base.IsOwner)
            return;

        var gamepad = Gamepad.current;

        if (gamepad != null && gamepad.enabled)
        {
            isGamepad = true;
            //Debug.Log("gamepad connected");
        }
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
    #endregion


    #region Get Input
    // Return the movement input
    public Vector2 inputMovement()
    {
        return movementInput;
    }

    // Return the look input
    public Vector2 inputLook()
    {
        return lookInput;
    }

    // Return the Left Action input
    public bool inputActionL()
    {
        return actionL;
    }

    // Return the Right Action input
    public bool inputActionR()
    {
        return actionR;
    }

    public bool isGamepadUsed()
    {
        return isGamepad;
    }
    #endregion
}
