using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnManager : MonoBehaviour
{
    [Header("NPCs")]
    [SerializeField] private int npcToSpawn;
    [SerializeField] private GameObject[] npcPrefabs;

    [Header("Clusters")]
    [SerializeField] private Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < npcToSpawn; i++)
        {
            int j = Random.Range(0, npcPrefabs.Length);
            int k = Random.Range(0, spawnPoints.Length);
            Instantiate(npcPrefabs[j], spawnPoints[k]);
        }

        for(int i = 0; i < spawnPoints.Length; i++)
        {
            for(int j = 0; j < spawnPoints[i].childCount; j++)
            {
                spawnPoints[i].GetChild(j).transform.position = spawnPoints[i].transform.position + new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
            }
        }
    }
}
