using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCLifeBehaviour : MonoBehaviour
{
    private bool isInfected = false;
    
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private AudioSource deathAudio;
    private NPCAnimationBehaviour npcAnimation;

    // Start is called before the first frame update
    void Start()
    {
        npcAnimation = GetComponentInChildren<NPCAnimationBehaviour>();
    }

    public void Death(Vector3 splashDirection)
    {
        deathParticles.transform.forward = splashDirection;
        deathAudio.Play();
        deathParticles.Play();
        npcAnimation.DeathRagdoll();
        Destroy(GetComponent<NavMeshAgent>());
        Destroy(GetComponent<NPCMovementBehaviours>());
    }

    public void Infection()
    {
        isInfected = true;
    }

    public bool isNPCInfected()
    {
        return isInfected;
    }
}
