using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawns;
    private int i = 0;

    public void Spawn(GameObject player)
    {
        player.transform.position = spawns[i].transform.position;
        i++;
    }
}
