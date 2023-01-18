using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMovementBehaviour : MonoBehaviour
{
    private SplitScreenInputHandler input;
    private Rigidbody rb;
    private Camera cam;

    [Header("Crawl")]
    [SerializeField] private float crawlSpeed = 1f;
    [SerializeField] private bool canCrawl = true;

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
        input = GetComponent<SplitScreenInputHandler>();
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isGrounded());

        if (canJump && isGrounded())
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

        if(isGrounded())
        {
            Crawl();
        }
    }

    private void Crawl()
    {
        Vector3 crawl = transform.forward * input.inputMovement().y * crawlSpeed;
        rb.velocity = crawl;
    }

    // Make player jump
    private void Jump()
    {
        // set jump dir
        jumpDir = cam.transform.forward * jump;
        jumpDir.y += jumpHeight;

        // apply jump
        rb.velocity = jumpDir;
        jump = 0;

        // reset jump after delay
        StartCoroutine(JumpDelay(jumpingTimer));
    }

    public void ExitNPC()
    {
        // set jump dir
        jumpDir = cam.transform.forward * 10f;
        jumpDir.y += jumpHeight;

        // apply jump
        rb.velocity = jumpDir;

        // reset jump after delay
        StartCoroutine(JumpDelay(jumpingTimer));
    }

    public void GetInNPC()
    {

    }

    // Return true if player is grounded
    public bool isGrounded()
    {
        Physics.Raycast(transform.position - Vector3.up * 0.01f, -Vector3.up, out RaycastHit groundHit, 0.2f);
        if(groundHit.collider || (rb.velocity.y <= 0.001f && rb.velocity.y >= -0.001f))
            return true;
        else
            return false;
    }

    public void Death()
    {

    }

    // Reset player's capacity to jump
    private IEnumerator JumpDelay(float delay)
    {
        canJump = false;
        yield return new WaitForSeconds(delay);
        canJump = true;
    }
}
