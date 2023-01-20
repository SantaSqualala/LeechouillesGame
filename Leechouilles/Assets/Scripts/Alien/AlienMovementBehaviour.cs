using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlienMovementBehaviour : MonoBehaviour
{
    private InputHandler input;
    private Rigidbody rb;
    private Camera cam;
    Animator animator;
    AlienUI alienUI;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 1f;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float jumpBuildSpeed = 5f;
    [SerializeField] private float minJumpPower = 3f;
    [SerializeField] private float maxJumpPower = 10f;
    [SerializeField] private float jumpingTimer = 1f;
    private bool canJump = true;
    private Vector3 jumpDir = Vector3.zero;
    private float jump = 0f;
    private bool isInNPC = false;
    private NPCLifeBehaviour infectedNPC;


    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        animator = GetComponentInChildren<Animator>();
        alienUI = GetComponent<AlienUI>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("NPC", isInNPC);

        alienUI.jumpRefill = jump / maxJumpPower;

        if (Physics.Raycast(transform.position, -Vector3.up, out RaycastHit ground, 0.1f) && rb.velocity.y < 1f && !ground.rigidbody && !isInNPC)
        {
            Move();
        }

        if (canJump)
        {
            BuildJump();
        }
    }

    #region movement
    private void BuildJump()
    {
        animator.SetBool("Jump", true);

        if(input.inputJump() && jump <= maxJumpPower)
        { 
            if(jump < minJumpPower)
                jump = minJumpPower; 
            else
                jump += Time.deltaTime * jumpBuildSpeed;
        }

        if (jump > maxJumpPower || (!input.inputJump() && jump >= minJumpPower))
        {
            Jump();
        }
    }

    // Make player jump
    private void Jump()
    {
        isInNPC = false;
        if(infectedNPC != null)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<CapsuleCollider>().isTrigger = true;
            infectedNPC.Death(transform.forward);
            infectedNPC = null;
            StartCoroutine(ResetColl(0.1f));
        }

        // set jump dir
        jumpDir = Vector3.forward * jump;
        jumpDir.y += jumpHeight;
        jumpDir = cam.transform.TransformDirection(jumpDir);

        // reset parent
        transform.SetParent(null, true);

        // apply jump
        rb.AddForce(jumpDir, ForceMode.Impulse);
        jump = 0;

        // reset jump after delay
        StartCoroutine(JumpDelay(jumpingTimer));
    }

    // Reset player's capacity to jump
    private IEnumerator JumpDelay(float delay)
    {
        canJump = false;
        yield return new WaitForSeconds(delay);
        canJump = true;
        animator.SetBool("Jump", false);
    }

    // Movement
    private void Move()
    {
        animator.SetBool("Jump", false);
        Vector3 move = transform.forward * input.inputMovement().y * moveSpeed - Vector3.up;
        rb.velocity = move;
        animator.SetFloat("MoveSpeed", rb.velocity.magnitude);
    }
    #endregion

    #region death & infection
    public void Death()
    {
        FindObjectOfType<GameManager>().AlienDeath();
        FindObjectOfType<HunterShootBehaviour>().AlienKilled();
        cam.transform.SetParent(null, true);
        this.enabled = false;
        Destroy(gameObject);
    }

    public void Infection(Transform npc)
    {
        GetComponent<Rigidbody>().isKinematic = true;
        transform.SetParent(npc, false);
        transform.position = npc.position + Vector3.up * 1.4f;
        isInNPC = true;
        infectedNPC = npc.GetComponent<NPCLifeBehaviour>();
        JumpDelay(10f);
    }

    IEnumerator ResetColl(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<CapsuleCollider>().isTrigger = false;
    }
    #endregion
}
