using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovementBehaviours : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float minMoveSpeed = 1.5f;
    [SerializeField] private float maxMoveSpeed = 4.5f;
    private NavMeshAgent agent;
    private float changeDestinationDelay;
    private float moveSpeed;
    private Vector3 destination;
    private bool changeDestination = false;

    [Header("Fleeing")]
    [SerializeField][Range(1f, 15f)] private float fleeDelay = 5f;
    [SerializeField] private float fleeDistance = 10f;
    private Vector3 fleeDirection;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        agent.speed = moveSpeed;
        ChangeDestination();
    }

    private void Update()
    {
        if(changeDestination)
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        destination = new Vector3(Random.Range(-60f, 60f), Random.Range(-2f, 20f), Random.Range(-60f, 60f));
        changeDestinationDelay = Random.Range(1f, 15f);
        agent.destination = destination;
        StartCoroutine(ChangeDestinationReset(changeDestinationDelay));
    }

    private IEnumerator ChangeDestinationReset(float delay)
    {
        changeDestination = false;
        yield return new WaitForSeconds(delay);
        changeDestination = true;
    }

    public void Flee(Vector3 deathPos)
    {
        fleeDirection = transform.position - deathPos;
        fleeDirection = fleeDirection.normalized;
        agent.destination = fleeDirection * fleeDistance;

        StartCoroutine(ChangeDestinationReset(fleeDelay));
    }
}
