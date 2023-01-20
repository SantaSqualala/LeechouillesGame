using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienPosBehaviour : MonoBehaviour
{
    public float duration = 2.5f;

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;
        GetComponentInChildren<MeshRenderer>().material.color = new Color(1, 0, 0, duration);

        if(duration <= 0)
        {
            Destroy(gameObject);
        }
    }
}
