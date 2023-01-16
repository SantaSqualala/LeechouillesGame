using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterBehaviour : MonoBehaviour
{
    private InputHandler inputHandler;
    private CharacterController characterController;
    private Camera cam;

    [Header("Hunter stats")]
    [SerializeField] private int health = 5;
    [SerializeField] private LayerMask alienMask;
    [SerializeField] private float attackDelay = 0.5f;
    [SerializeField] private float attackDistance = 1.5f;
    private bool canAttack = true;

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
        inputHandler = GetComponentInParent<InputHandler>();
        characterController = GetComponent<CharacterController>();
        cam = inputHandler.GetComponentInChildren<Camera>();
        cam.transform.SetParent(characterController.transform, false);
        cam.transform.localPosition = new Vector3(0f, 0.6f, 0f);
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();

        if(inputHandler.inputActionL())
        {
            SmackAliens();
        }
    }

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

    private void Movement()
    {
        Vector3 move = new Vector3(inputHandler.inputMovement().x, 0f, inputHandler.inputMovement().y).normalized;
        move = characterController.transform.TransformDirection(move) * walkSpeed * Time.deltaTime;
        move.y = Physics.gravity.y * Time.deltaTime;

        characterController.Move(move);

        StickToGround();
    }

    private void StickToGround()
    {
        Physics.Raycast(transform.position - new Vector3(0f, characterController.height / 2f, 0f), -Vector3.up, out RaycastHit ground, Mathf.Infinity, 0, QueryTriggerInteraction.Ignore);

        if(ground.collider && ground.point.y <= transform.position.y - characterController.height / 2f)
        {
            transform.position = ground.point + Vector3.up * characterController.height / 2f;
        }
    }

    private void SmackAliens()
    {
        if(canAttack)
        {
            RaycastHit hit, hitNPC;
            Physics.SphereCast(cam.transform.position, 0.3f, cam.transform.forward, out hit, attackDistance, alienMask);

            canAttack = false;
            StartCoroutine(AttackDelay(attackDelay));

            if(hit.rigidbody.GetComponent<AlienBehaviour>())
            {
                hit.rigidbody.GetComponent<AlienBehaviour>().Death();
            }

            Physics.SphereCast(cam.transform.position, 0.3f, cam.transform.forward, out hitNPC, attackDistance);
            if (hitNPC.rigidbody.GetComponent<NPCBehaviours>())
            {
                hitNPC.rigidbody.GetComponent<NPCBehaviours>().NPCDeath();
            }
            //Debug.Log(hit.rigidbody.gameObject.name);
        }
    }

    private IEnumerator AttackDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAttack = true;
    }
}
