using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlienPositionIndicatorBehaviour : MonoBehaviour
{
    private Camera hunterCam;

    private void Start()
    {
        hunterCam = FindObjectOfType<HunterMovementBehaviour>().GetComponentInChildren<Camera>();
    }

    private void OnPreCull()
    {
        this.transform.up = hunterCam.transform.forward;
    }
}
