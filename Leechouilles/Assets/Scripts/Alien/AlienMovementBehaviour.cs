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
    private bool isInNPC = false;
    private NPCLifeBehaviour infectedNPC;

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
        if(canJump)
            BuildJump();
    }

    #region movement
    private void BuildJump()
    {
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
            StartCoroutine(ResetColl(0.5f));
        }

        // set jump dir
        jumpDir = Vector3.forward * jump;
        jumpDir.y += jumpHeight;
        jumpDir = cam.transform.TransformDirection(jumpDir);

        // reset parent
        transform.SetParent(null, true);

        // apply jump
        rb.AddForce(jumpDir * jump, ForceMode.Impulse);
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
    }
    #endregion

    #region death & infection
    public void Death()
    {
        cam.transform.SetParent(null, true);
        this.enabled = false;
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
