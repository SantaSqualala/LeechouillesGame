using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRandomMaterial : MonoBehaviour
{
    public Material[] mat;
    Material m;

    // Start is called before the first frame update
    void Start()
    {
        m = mat[Random.Range(0, mat.Length)];
        GetComponent<SkinnedMeshRenderer>().material = m;
        Destroy(this);
    }
}
