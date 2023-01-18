using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image alienJumpReload;
    [SerializeField] private Image scanReload;
    [SerializeField] private Image amunition;
    [SerializeField] private Image alienCross;

    //public values
    public float ReloadTime;
    public float ReloadTimeMax;

    public float amunitionsLeft;
    public float amunitionMax;

    public float alienLeft;
    public float alienMax;

    public float alienJumpLeft;
    public float alienJumpMax;

    void Start()
    {
        scanReload.fillAmount = 0;
        scanReload.fillAmount = 1;
        alienCross.fillAmount = 1;
    }

    void Update()
    {
        scanReload.fillAmount = ReloadTime/ReloadTimeMax;
        amunition.fillAmount = amunitionsLeft/amunitionMax;
        alienCross.fillAmount = alienLeft/alienMax;
        alienJumpReload.fillAmount = alienJumpLeft / alienJumpMax;
    }
}
