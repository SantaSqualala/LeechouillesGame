using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NetAlienBehaviour : NetworkBehaviour
{
    private InputHandler inputHandler;
    private Rigidbody rb;

    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform playerCamParent;
    [SerializeField] private float xRotSpeed = 100f;
    [SerializeField] private float yRotSpeed = 100f;
    [SerializeField] private float xMinRot = -70f;
    [SerializeField] private float xMaxRot = 70f;
    private float xRot;

    [Header("Alien Insider")]
    [SerializeField] private float timeBeforeDeath = 15f;
    [SerializeField] private float timeBeforeEjection = 20f;
    [SerializeField] private float timeBeforeNewJump = 3f;
    [SerializeField] private float minJumpPower = 3f;
    [SerializeField] private float maxJumpPower = 10f;
    private Vector3 jump;
    private float jumpPower;
    private bool isInNPC;
    private GameObject npcHolder;
    private bool canJump;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if(!base.IsOwner)
        {
            Destroy(GetComponent<NetAlienBehaviour>());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.enabled = false;

        inputHandler = GetComponent<InputHandler>();
        cam = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate is called once per physics tick
    void FixedUpdate()
    {
        Movement();
        Look();
    }

    void Movement()
    {
        if (canJump && inputHandler.inputActionL())
        {
            if (jumpPower <= maxJumpPower)
            {
                if (jumpPower > minJumpPower)
                {
                    jumpPower += Time.deltaTime;
                }
                else
                {
                    jumpPower = minJumpPower;
                }
            }
            else
            {
                Jump();
            }

            jump = cam.transform.forward * jumpPower;
        }
        else if (!inputHandler.inputActionL())
        {
            if (jumpPower >= minJumpPower)
            {
                Jump();
            }

            jumpPower = 0f;
        }
    }

    void Look()
    {
        // look variables
        float xcam = inputHandler.inputLook().x;
        float ycam = inputHandler.inputLook().y;

        // transform camera direction
        xRot -= ycam * xRotSpeed * Time.deltaTime;
        xRot = Mathf.Clamp(xRot, xMinRot, xMaxRot);
        playerCamParent.transform.localRotation = Quaternion.Euler(new Vector3(xRot, 0f, 0f));

        // transform body direction
        transform.Rotate(Vector3.up * xcam * Time.deltaTime);
    }

    void Jump()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<NavMeshAgent>())
        {

        }
    }
}
