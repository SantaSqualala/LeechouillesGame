using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MovementBehaviour
{
    StayInPlace = 0,
    MoveFreely,
    MoveInSpawnRange,
    GoalInMind
}

public class NPCMovementBehaviour : MonoBehaviour
{
    private MovementBehaviour movementBehaviour;

    [Header("Movement")]
    [SerializeField] private float minMoveSpeed = 1.5f;
    [SerializeField] private float maxMoveSpeed = 4.5f; 
    private NavMeshAgent agent;
    private float changeDestinationDelay;
    private float moveSpeed;
    private Vector3 destination;
    private bool changeDestination = false;

    [Header("Fleeing")]
    [SerializeField] [Range(1f, 15f)] private float fleeDelay = 5f;
    private Vector3 fleeDirection;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetMoveBehaviour((MovementBehaviour)Random.Range(0, 4));

        Debug.Log("Move behaviour : " + movementBehaviour.ToString());
    }

    public void SetMoveBehaviour(MovementBehaviour behaviour)
    {
        movementBehaviour = behaviour;

        switch(movementBehaviour)
        {
            case MovementBehaviour.StayInPlace:
                // set movement speed to 0
                moveSpeed = 0f;
                agent.speed = moveSpeed;
                // prevent movement
                changeDestinationDelay = 0f;
                changeDestination = false;
                // in case of fleeing : can reset position
                destination = transform.position;
                break;

            case MovementBehaviour.MoveFreely:
                // set random move speed
                moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
                agent.speed = moveSpeed;
                // set random destination change delay
                changeDestinationDelay = Random.Range(fleeDelay, 20f);
                ChangeDestinationRandom(new Vector3(-60f, -1f, -60f), new Vector3(-60f, 20f, 60f));
                break;

            case MovementBehaviour.MoveInSpawnRange:
                moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
                agent.speed = moveSpeed;
                break;

            case MovementBehaviour.GoalInMind:
                moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
                agent.speed = moveSpeed;
                break;

            default:
                moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
                agent.speed = moveSpeed;
                break;
        }
    }

    // Change ai destination to random vector
    private void ChangeDestinationRandom(Vector3 min, Vector3 max)
    {
        agent.destination = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));

        if(!agent.pathPending && !agent.hasPath)
        {
            if (changeDestinationDelay > 0f)
            {
                StartCoroutine(ChangeDestinationDelay(changeDestinationDelay));
            }
        }
    }

    // Change ai destination to specific vector
    private void ChangerDestination(Vector3 destination)
    {

    }

    // 
    private IEnumerator ChangeDestinationDelay(float delay)
    {
        changeDestination = false;
        yield return new WaitForSeconds(delay);
        changeDestination = true;
    }
}
