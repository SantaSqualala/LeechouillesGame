using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float bulletDrop = 3f;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * moveSpeed - Vector3.up * bulletDrop;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject coll = collision.gameObject;

        // kill alien
        if(coll.GetComponent<AlienMovementBehaviour>())
        {
            coll.GetComponentInChildren<AlienLifeBehaviour>().Death();
        }

        // kill npc
        if(coll.GetComponent<NPCLifeBehaviour>())
        {
            coll.GetComponent<NPCLifeBehaviour>().Death(-collision.transform.forward);

            // kill alien if inside an npc
            if(coll.GetComponent<NPCLifeBehaviour>().isNPCInfected())
            {
                coll.GetComponentInChildren<AlienLifeBehaviour>().Death();
            }
        }

        // destroy
        Destroy(gameObject);
    }
}
