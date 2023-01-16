using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMovementBehaviour : MonoBehaviour
{
    private InputHandler input;
    private Rigidbody rb;
    private Camera cam;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float jumpBuildSpeed = 5f;
    [SerializeField] private float minJumpPower = 3f;
    [SerializeField] private float maxJumpPower = 10f;
    [SerializeField] private float jumpingTimer = 1f;
    private bool canJump = true;
    private Vector3 jumpDir = Vector3.zero;
    private float jump = 0f;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canJump && isGrounded())
        {
            if(input.inputJump())
            {
                // set jump to minpower at input pressed
                if(jump <= minJumpPower)
                {
                    jump = minJumpPower;
                }

                // increment jump
                jump += jumpBuildSpeed * Time.deltaTime;

                // launch jump if its sup to maxpower
                if(jump >= maxJumpPower)
                {
                    Jump();
                }
            }

            // trigger jump if input is released and jump has enough value
            if(!input.inputJump() && jump >= minJumpPower)
            {
                Jump();
            }
        }
    }

    // Make player jump
    private void Jump()
    {
        // set jump dir
        jumpDir = cam.transform.forward * jump;
        jumpDir.y += jumpHeight;

        // reset parent
        transform.SetParent(null, true);

        // apply jump
        rb.AddForce(jumpDir * jump);
        jump = 0;

        // reset jump after delay
        StartCoroutine(JumpDelay(jumpingTimer));
    }

    // Return true if player is grounded
    public bool isGrounded()
    {
        return Physics.Raycast(transform.position - new Vector3(0f, GetComponent<BoxCollider>().bounds.extents.y, 0f), -Vector3.up, 0.1f);
    }

    // Reset player's capacity to jump
    private IEnumerator JumpDelay(float delay)
    {
        canJump = false;
        yield return new WaitForSeconds(delay);
        canJump = true;
    }
}
