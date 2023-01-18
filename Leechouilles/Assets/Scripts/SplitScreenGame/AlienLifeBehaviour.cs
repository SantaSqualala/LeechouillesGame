using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienLifeBehaviour : MonoBehaviour
{
    [SerializeField] private float lifeOutsideDuration = 15f;
    [SerializeField] private float lifeInsideDuration = 20f;
    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Death()
    {
        FindObjectOfType<PlayerVictoryManager>().AlienDead(this.gameObject);
        isAlive = false;

        Destroy(GetComponent<AlienMovementBehaviour>());
        Destroy(GetComponentInChildren<AlienPositionIndicatorBehaviour>().gameObject);
    }
}
