using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlienCameraBehaviour : MonoBehaviour
{
    private Transform cameraHolder;
    private InputHandler inputHandler;

    [Header("Camera controls")]
    [SerializeField] private float xCameraSensitivity = 100f;
    [SerializeField] private float yCameraSensitivity = 100f;
    [SerializeField] private float minXRot = -80f;
    [SerializeField] private float maxXRot = 80f;
    private float xRot = 0f;

    [Header("Camera Keyboard vs Gamepad")]
    [SerializeField] private float rotSpeedKeyboard = 0.3f;
    [SerializeField] private float rotSpeedGamepad = 1f;
    float lookSpeed = 1f;


    private void Start()
    {
        cameraHolder = transform.parent;
        inputHandler = GetComponentInParent<InputHandler>();

        if (GetComponentInParent<PlayerInput>().currentControlScheme == "Keyboard")
            lookSpeed = rotSpeedKeyboard;
        else
            lookSpeed = rotSpeedGamepad;
    }

    private void LateUpdate()
    {
        TPSCameraLook();
    }

    // Rotate the camera and its parent accordingly to input
    private void TPSCameraLook()
    {
        cameraHolder.transform.parent.Rotate(Vector3.up * inputHandler.inputLook().x * yCameraSensitivity * Time.deltaTime * lookSpeed);

        xRot -= inputHandler.inputLook().y * xCameraSensitivity * Time.deltaTime * lookSpeed;
        xRot = Mathf.Clamp(xRot, minXRot, maxXRot);
        cameraHolder.transform.localRotation = Quaternion.Euler(new Vector3(xRot, 0f, 0f));
    }
}
