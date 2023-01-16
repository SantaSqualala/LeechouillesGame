using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    private List<NPCBehaviours> npcs = new List<NPCBehaviours>(); 

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            npcs.Add(transform.GetChild(i).GetComponent<NPCBehaviours>());
            npcs[i].transform.position += Vector3.left * Random.Range(-4f, 4f) + Vector3.forward * Random.Range(-4f, 4f) ;
        }
    }

    public void NPCDEAD(NPCBehaviours npc)
    {
        npcs.Remove(npc);
        npc.GetComponentInChildren<Animator>().transform.SetParent(transform, true);
        Destroy(npc);
    }
}
