using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CestTrivial : MonoBehaviour
{
    public AudioSource trivial;
    [Range(15f, 60f)] public float delay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Trivial(delay));
    }

    IEnumerator Trivial(float delay)
    {
        yield return new WaitForSeconds(delay);
        trivial.Play();
        StartCoroutine(Trivial(delay));
    }
}
