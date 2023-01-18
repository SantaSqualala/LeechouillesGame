using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BulletBehaviour : MonoBehaviour
{
    public float bulletSpeed = 25f;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other);

        other.TryGetComponent(out AlienMovementBehaviour alien);
        if(alien != null)
            alien.Death();

        other.TryGetComponent(out NPCLifeBehaviour npc);
        if (npc != null)
            npc.Death(this.transform.forward);
        
        Destroy(this.gameObject);
    }
}
