using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawns;
    [SerializeField] private TMP_Text announcer;
    private int i = 0;

    public void Spawn(GameObject player)
    {
        player.transform.position = spawns[i].transform.position;
        player.transform.rotation = spawns[i].transform.rotation;
        i++;

        if(i >= 2)
        {
            announcer.gameObject.SetActive(false);
        }
    }
}
