using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NPCRandomDeathSound : MonoBehaviour
{
    public AudioClip[] audios;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().clip = audios[Random.Range(0, audios.Length)];
    }
}
