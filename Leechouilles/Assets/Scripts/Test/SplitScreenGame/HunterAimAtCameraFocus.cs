using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAimAtCameraFocus : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private Vector3 pos;

    // Update is called once per frame
    void Update()
    {
        pos = cam.transform.position + cam.transform.forward * 50f;
        transform.LookAt(pos);

        //Debug.Log(pos);
    }
}
