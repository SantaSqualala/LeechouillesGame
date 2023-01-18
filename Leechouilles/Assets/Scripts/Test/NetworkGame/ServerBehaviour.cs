using FishNet.Transporting.Tugboat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ServerBehaviour : MonoBehaviour
{
    [SerializeField] private Tugboat tBoat;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] Canvas connexionUI;
    private string ipEntered;

    public void GetIPAdress()
    {
        ipEntered = inputField.text;
        tBoat.SetClientAddress(ipEntered);
        print(tBoat.GetClientAddress());
    }

    public void DeActivateConnexionUI()
    {
        connexionUI.gameObject.SetActive(false);
    }

    public void ActivateConnexionUI()
    {
        connexionUI.gameObject.SetActive(true);
    }
}
