using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public List<GameObject> npcs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            npcs.Add(transform.GetChild(i).gameObject);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            npcs[0].GetComponent<NPCLifeBehaviour>().Death(Vector3.one); ;
        }
    }

    public void NPCDeath(GameObject npc)
    {
        npcs.Remove(npc);

        foreach(GameObject t in npcs)
        {
            if(Vector3.Distance(t.transform.position, npc.transform.position) < 8f)
                t.GetComponent<NPCMovementBehaviours>().Flee(npc.transform.position);
        }
    }
}
