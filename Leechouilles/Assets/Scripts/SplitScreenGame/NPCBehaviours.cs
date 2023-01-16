using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum NPCMovementHabits
{
    FreeMove = 0,
    Static,
    GoalInMind
}

public class NPCBehaviours : MonoBehaviour
{
    [Header("NPC movement")]
    [SerializeField] private NPCMovementHabits movementHabits;
    [SerializeField] private float minMoveSpeed = 1.5f;
    [SerializeField] private float maxMoveSpeed = 4.5f;
    [SerializeField] private float fleeDelay = 5f;
    [SerializeField] private float fleeSpeed = 5f;
    private NavMeshAgent agent;
    private float moveSpeed;
    private Vector3 fleeDirection;
    private Vector3 destination;
    private bool changeDir = true;

    [Header("NPC death")]
    [SerializeField] private LayerMask npcCollisionMask;
    [SerializeField] private float explosionFleeRadius = 8f;
    [SerializeField] private ParticleSystem explosionDeath;
    [SerializeField] private AudioSource explosionSound;
    private List<NPCBehaviours> npcsInRangeForFleeOnDeath = new List<NPCBehaviours>();

    // infection vars
    private bool infected = false;
    private AlienBehaviour alienInside = null;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        infected = false;
        alienInside = null;
        
        SetMoveset((NPCMovementHabits)Random.Range(0, 3));
    }

    // Update is called once per frame
    void Update()
    {
        if(!infected)
        {
            if (changeDir)
            {
                SearchDestination();
            }
        }
    }

    #region Move
    private void SearchDestination()
    {
        destination = RandomVector();
        agent.destination = destination;
        changeDir = false;
        StartCoroutine(SearchDestinationReset(5f));
    }

    private IEnumerator SearchDestinationReset(float delay)
    {
        yield return new WaitForSeconds(delay);
        changeDir = true;
    }

    private Vector3 RandomVector()
    {
        return new Vector3(Random.Range(-30f, 30f), Random.Range(-1f, 20f), Random.Range(-70f, 20f));
    }
    public void SetMoveset(NPCMovementHabits moveset)
    {
        movementHabits = moveset;

        switch(movementHabits)
        {
            case NPCMovementHabits.FreeMove:
                moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
                SearchDestination();
                break;
            case NPCMovementHabits.Static:
                moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
                SearchDestination();                
                break;
            case NPCMovementHabits.GoalInMind:
                moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
                SearchDestination();
                changeDir = false;
                break;
            default:
                moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
                SearchDestination();
                break;
        }

        agent.speed = moveSpeed;
    }
    #endregion

    #region Infection & death
    // Triggers when the alien enters the body
    public void AlienInside(AlienBehaviour alien)
    {
        infected = true;
        alienInside = alien;
        Destroy(GetComponent<NavMeshAgent>());
    }

    // NPC will flee in the opposite direction of the death
    private void NPCDeathNearby(Vector3 deathPosition)
    {
        fleeDirection = transform.position - deathPosition;
        destination = fleeDirection;
        agent.speed = fleeSpeed;

        StartCoroutine(StopFleeing(fleeDelay));
    }

    // Triggers events when the alien leaves the body
    public void NPCDeath()
    {
        //Instantiate(explosionDeath, transform.position, alienInside.transform.rotation);
        //explosionSound.Play();
        //Destroy(animator);

        if(npcsInRangeForFleeOnDeath.Count > 0)
        {
            foreach (var npc in npcsInRangeForFleeOnDeath)
            {
                npc.NPCDeathNearby(transform.position);
            }
        }
    }

    IEnumerator StopFleeing(float delay)
    {
        yield return new WaitForSeconds(delay);
        agent.speed = moveSpeed;
        SearchDestination();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NPCBehaviours>())
        {
            npcsInRangeForFleeOnDeath.Add(other.GetComponent<NPCBehaviours>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<NPCBehaviours>())
        {
            npcsInRangeForFleeOnDeath.Remove(other.GetComponent<NPCBehaviours>());
        }
    }
    #endregion
}
