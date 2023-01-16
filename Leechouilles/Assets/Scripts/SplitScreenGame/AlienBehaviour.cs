using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBehaviour : MonoBehaviour
{
    private InputHandler inputHandler;
    private Camera cam;
    private Rigidbody rb;
    private bool isAlive = true;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float minJumpPower = 3f;
    [SerializeField] private float maxJumpPower = 10f;
    [SerializeField] private float jumpIncrementSpeed = 2f;
    [SerializeField] private float jumpDelay;
    private bool canJump = true;
    private float jump = 0f;

    [Header("Camera")]
    [SerializeField] private float xCameraSensitivity = 100f;
    [SerializeField] private float yCameraSensitivity = 100f;
    [SerializeField] private float mouseFactor = 10f;
    [SerializeField] private float minXRot = -80f;
    [SerializeField] private float maxXRot = 80f;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0f, 2.1f, -1.5f);
    [SerializeField] private Transform cameraHolder;
    private float xRot = 0f;

    [Header("NPC Hosting")]
    [SerializeField] private float maxTimeInNpc;
    [SerializeField] private float maxTimeOutsideNpc;
    private bool isInNPC;
    private NPCBehaviours npcHost;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponentInParent<InputHandler>();
        rb = GetComponent<Rigidbody>();
        cam = inputHandler.GetComponentInChildren<Camera>();
        cam.transform.SetParent(cameraHolder, false);
        cam.transform.localPosition = cameraOffset;
    }

    // Update is called once per frame
    private void Update()
    {
        if(isAlive)
        {
            if(inputHandler.inputActionR() && canJump)
            {
                LoadJump();
            }

            if (!inputHandler.inputActionR() && canJump && jump >= minJumpPower)
            {
                Jump();
            }

            if(!isInNPC)
            {
                Move();
            }
        }
    }

    private void LateUpdate()
    {
        TPSCameraLook();
    }

    #region Movement
    private void Jump()
    {
        rb.isKinematic = false;

        canJump = false;
        StartCoroutine(JumpingDelay(jumpDelay));

        if(isInNPC)
        {
            LeaveNPC();
        }

        rb.velocity = cam.transform.forward * jump;
        jump = 0f;
    }

    private void Move()
    {
        Vector3 move = transform.forward * inputHandler.inputMovement().y + transform.right * inputHandler.inputMovement().x;
        move = move.normalized;
        move *= moveSpeed;

        rb.velocity = move;
    }

    private void LoadJump()
    {
        if (jump < minJumpPower)
        {
            jump = minJumpPower;
        }

        if (jump < maxJumpPower)
        {
            jump += jumpIncrementSpeed * Time.deltaTime;
        }
    }

    private void TPSCameraLook()
    {
        transform.Rotate(Vector3.up * inputHandler.inputLook().x * yCameraSensitivity * Time.deltaTime);

        xRot -= inputHandler.inputLook().y * xCameraSensitivity * Time.deltaTime;
        xRot = Mathf.Clamp(xRot, minXRot, maxXRot);
        cameraHolder.transform.localRotation = Quaternion.Euler(new Vector3(xRot, 0f, 0f));
    }

    private IEnumerator JumpingDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canJump = true;
    }
    #endregion

    #region Death
    public void Death()
    {
        Destroy(this);
        cam.transform.SetParent(Camera.main.transform, false);
    }
    #endregion

    #region Enter/Leave NPC
    private void EnterNPC(NPCBehaviours npc)
    {
        npcHost = npc;
        rb.transform.SetParent(npcHost.transform, false);
        rb.transform.localPosition = new Vector3(0f, 1.2f, 0f);
        rb.isKinematic = true;
    }

    private void LeaveNPC()
    {
        isInNPC = false;
        npcHost.NPCDeath();
        transform.SetParent(inputHandler.transform, true);
        npcHost = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<NPCBehaviours>())
        {
            collision.gameObject.GetComponent<NPCBehaviours>().AlienInside(this);
            EnterNPC(collision.gameObject.GetComponent<NPCBehaviours>());
        }
    }
    #endregion
}
