using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienCameraBehaviour : MonoBehaviour
{
    private SplitScreenInputHandler inputHandler;

    [Header("Camera controls")]
    [SerializeField] private float xCameraSensitivity = 100f;
    [SerializeField] private float yCameraSensitivity = 100f;
    [SerializeField] private float minXRot = -80f;
    [SerializeField] private float maxXRot = 80f;
    private float xRot = 0f;
    private float yRot = 0f;

    private void Start()
    {
        inputHandler = GetComponentInParent<SplitScreenInputHandler>();
    }

    private void LateUpdate()
    {
        TPSCameraLook();
    }

    // Rotate the camera and its parent accordingly to input
    private void TPSCameraLook()
    {
        transform.parent.Rotate(Vector3.up * inputHandler.inputLook().x * yCameraSensitivity * Time.deltaTime);
        //yRot += inputHandler.inputLook().x * yCameraSensitivity * Time.deltaTime;
        xRot -= inputHandler.inputLook().y * xCameraSensitivity * Time.deltaTime;
        xRot = Mathf.Clamp(xRot, minXRot, maxXRot);
        transform.localRotation = Quaternion.Euler(new Vector3(xRot, yRot, 0f));
    }
}
