using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class NetHunterBehaviour : NetworkBehaviour
{
    private NetInputHandler netInputHandler;
    private Camera cam;
    private CharacterController characterController;

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float speedMult = 1f;
    private Vector3 movement;

    [Header("Camera")]
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float yRotationSpeed = 100f;
    [SerializeField] private float xRotationSpeed = 100f;
    [SerializeField] private float xRotLimiter = 80;
    private float xRot;


    public override void OnStartClient()
    {
        base.OnStartClient();
        if(!base.IsOwner)
        {
            Destroy(GetComponent<NetHunterBehaviour>());
            return;
        }

        if(base.IsServer)
        {
            NetPlayerBehaviour.instance.players.Add(gameObject.GetInstanceID(), new NetPlayerBehaviour.Player() { health = 1 });
        }
    }

    private void Start()
    {
        netInputHandler = GetComponent<NetInputHandler>();
        cam = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();
    }

    private void LateUpdate()
    {
        Look();
    }

    [Client(RequireOwnership = true)]
    void Movement()
    {
        movement = new Vector3(netInputHandler.inputMovement().x, Physics.gravity.y, netInputHandler.inputMovement().y).normalized;
        movement = movement * movementSpeed * speedMult * Time.deltaTime;
        movement = transform.TransformDirection(movement);

        characterController.Move(movement);
    }

    [Client(RequireOwnership = true)]
    void Look()
    {
        transform.Rotate(Vector3.up * netInputHandler.inputLook().x * yRotationSpeed * Time.deltaTime);

        xRot -= netInputHandler.inputLook().y * xRotationSpeed * Time.deltaTime;
        xRot = Mathf.Clamp(xRot, -xRotLimiter, xRotLimiter);
        cam.transform.localRotation = Quaternion.Euler(new Vector3(xRot, 0f, 0f));
    }
}
