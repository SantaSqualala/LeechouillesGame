using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienLifeBehaviour : MonoBehaviour
{
    [SerializeField] private float lifeOutsideDuration = 15f;
    [SerializeField] private float lifeInsideDuration = 20f;
    private bool isAlive = true;
    private bool isInNPC = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        if(collision.gameObject.GetComponentInChildren<NPCLifeBehaviour>() || collision.gameObject.GetComponentInParent<NPCLifeBehaviour>())
        {
            collision.gameObject.GetComponentInChildren<NPCLifeBehaviour>().Infection();
            transform.SetParent(collision.transform, false);
            isInNPC = true;
        }
    }

    public void ExitNPC()
    {
        isInNPC = false;
        StopCoroutine(ForceExitNPC(0f));
    }

    public void Death()
    {
        FindObjectOfType<PlayerVictoryManager>().AlienDead(gameObject);
        isAlive = false;

        Destroy(GetComponent<AlienMovementBehaviour>());
        Destroy(GetComponentInChildren<AlienPositionIndicatorBehaviour>().gameObject);
    }

    private IEnumerator ForceExitNPC(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<AlienMovementBehaviour>().ExitNPC();
    }
}
