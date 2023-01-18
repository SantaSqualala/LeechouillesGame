using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlienLifeBehaviour : MonoBehaviour
{
    [SerializeField] private TMP_Text timeLeftText;
    [SerializeField] private float lifeOutsideDuration = 15f;
    [SerializeField] private float lifeInsideDuration = 20f;
    private bool isAlive = true;
    private bool isInNPC = false;
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = lifeOutsideDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            {
                Death();
            }
        }

        //timeLeftText.text = "" + (int)timeLeft;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);

        if(collision.gameObject.GetComponentInChildren<NPCLifeBehaviour>() || collision.gameObject.GetComponentInParent<NPCLifeBehaviour>())
        {
            EnterNPC(collision.gameObject);
        }
    }

    public void ExitNPC()
    {
        Debug.Log("exit");

        isInNPC = false;
        timeLeft = lifeOutsideDuration;

        // re enables rb
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<AlienMovementBehaviour>().enabled = true;
        StopCoroutine(ForceExitNPC(0f));
    }

    private void EnterNPC(GameObject npc)
    {
        Debug.Log("enter");

        // play npc infection function
        npc.GetComponentInChildren<NPCLifeBehaviour>().Infection();
        transform.SetParent(npc.transform, false);
        isInNPC = true;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<AlienMovementBehaviour>().enabled = false;
        timeLeft = lifeInsideDuration;
        StartCoroutine(ForceExitNPC(lifeInsideDuration));
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
