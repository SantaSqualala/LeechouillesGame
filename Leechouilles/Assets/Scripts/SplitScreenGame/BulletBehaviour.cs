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
        Debug.Log(collision.gameObject.name);

        if(collision.gameObject.GetComponent<AlienMovementBehaviour>())
        {
            Destroy(collision.gameObject.GetComponent<AlienMovementBehaviour>());
        }

        Destroy(gameObject);
    }
}
