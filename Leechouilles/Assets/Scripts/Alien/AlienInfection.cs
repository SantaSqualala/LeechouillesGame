using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienInfection : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.TryGetComponent(out NPCLifeBehaviour npc);
        if(npc != null)
        {
            npc.Infection();
            GetComponent<AlienMovementBehaviour>().Infection(npc.transform);
        }

    }
}
