using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAimAnimationBehaviour : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Transform transformToOrient;
    [SerializeField] private float smoothing = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transformToOrient.forward = Vector3.Lerp(transform.forward, cam.transform.forward, smoothing);
    }
}
