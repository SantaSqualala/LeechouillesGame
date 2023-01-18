using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterMovementBehaviour : MonoBehaviour
{
    private InputHandler inputHandler;
    private CharacterController characterController;
    private Camera cam;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float runSpeed = 6f;

    [Header("Camera")]
    [SerializeField] private float xCameraSensitivity = 100f;
    [SerializeField] private float yCameraSensitivity = 100f;
    [SerializeField] private float mouseFactor = 10f;
    [SerializeField] private float minXRot = -80f;
    [SerializeField] private float maxXRot = 80f;
    private float xRot = 0f;

    // Start is called before the first frame update
    private void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        characterController = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    #region Camera Look
    private void LateUpdate()
    {
        FPSCameraLook();
    }

    private void FPSCameraLook()
    {
        transform.Rotate(Vector3.up * inputHandler.inputLook().x * yCameraSensitivity * Time.deltaTime);

        xRot -= inputHandler.inputLook().y * xCameraSensitivity * Time.deltaTime;
        xRot = Mathf.Clamp(xRot, minXRot, maxXRot);
        cam.transform.localRotation = Quaternion.Euler(new Vector3(xRot, 0f, 0f));
    }
    #endregion

    #region Move
    private void Movement()
    {
        Vector3 move = new Vector3(inputHandler.inputMovement().x, 0f, inputHandler.inputMovement().y).normalized;
        move = characterController.transform.TransformDirection(move) * walkSpeed * Time.deltaTime;
        move.y = Physics.gravity.y * Time.deltaTime;

        characterController.Move(move);

        //StickToGround();
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();
    }
    #endregion
}