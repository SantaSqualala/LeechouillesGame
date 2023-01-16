using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object;

public class NetCameraBehaviour : NetworkBehaviour
{
    private ServerBehaviour serverBehaviour;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if(base.IsOwner)
        {
            serverBehaviour = FindObjectOfType<ServerBehaviour>();
            serverBehaviour.DeActivateConnexionUI();

            GetComponent<Camera>().enabled = true;
            GetComponent<AudioListener>().enabled = true;
            Camera.main.gameObject.SetActive(false);
        }
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        if(base.IsOwner)
        {
            serverBehaviour = FindObjectOfType<ServerBehaviour>();
            serverBehaviour.ActivateConnexionUI();

            Camera.main.gameObject.SetActive(true);
        }
    }
}
