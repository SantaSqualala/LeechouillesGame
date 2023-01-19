using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(CharacterController))]

public class AlienMoveBehaviour : MonoBehaviour
{

    [SerializeField] private float SpeedRotKeyboard = 10;
    [SerializeField] private float SpeedRotController = 10;
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    private Vector2 movementInput;
    private Vector2 rotateInput;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;
    private bool sprintInput;
    private bool jumpInput;

    PlayerInput playerInput;
    InputHandler input;
    [HideInInspector] public bool play = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        input = GetComponent<InputHandler>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerInput = GetComponent<PlayerInput>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if(play)
            movementInput = ctx.ReadValue<Vector2>();
    }

    public void OnRotate(InputAction.CallbackContext ctx)
    {
        if (play)
            rotateInput = ctx.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext ctx)
    {
        if (play)
            sprintInput = ctx.action.triggered;
    }
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (play)
            jumpInput = ctx.action.triggered;
    }

    void Update()
    {
        if (playerInput.GetComponent<PlayerInput>().currentControlScheme == "Keyboard")
        {
            lookSpeed = SpeedRotKeyboard;
        }
        else lookSpeed = SpeedRotController;

        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward );
        Vector3 right = transform.TransformDirection(Vector3.right );
        // Press Left Shift to run
       
        float curSpeedX = canMove ? (sprintInput ? runningSpeed : walkingSpeed) * input.inputMovement().y : 0;
        float curSpeedY = canMove ? (sprintInput ? runningSpeed : walkingSpeed) * input.inputMovement().x : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (input.inputJump() && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += input.inputLook().y * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, input.inputLook().x * lookSpeed, 0);
        }
    }
}