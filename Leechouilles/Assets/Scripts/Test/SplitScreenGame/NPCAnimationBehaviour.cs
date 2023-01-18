using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAnimationBehaviour : MonoBehaviour
{
    private List<Rigidbody> ragRBs = new List<Rigidbody>();
    private List<CharacterJoint> ragJoints = new List<CharacterJoint>();
    private NavMeshAgent agent;
    private Animator animator;
    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponentInParent<NavMeshAgent>();

        foreach(var i in GetComponentsInChildren<Rigidbody>())
        {
            ragRBs.Add(i);
        }
        foreach (var i in GetComponentsInChildren<CharacterJoint>())
        {
            ragJoints.Add(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive)
        {
            animator.SetFloat("MoveSpeed", agent.velocity.magnitude);

            if (Input.GetKeyDown(KeyCode.Backspace))
                DeathRagdoll();
        }
    }

    public void DeathRagdoll()
    {
        transform.SetParent(null, true);
        Destroy(animator);
        isAlive = false;

        foreach(var i in ragRBs)
        {
            i.isKinematic = false;
        }

        StartCoroutine(StopRagdoll(10f));
    }

    private IEnumerator StopRagdoll(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (var i in ragJoints)
        {
            Destroy(i);
        }
        foreach (var i in ragRBs)
        {
            Destroy(i);
        }

        Destroy(this);
    }
}
