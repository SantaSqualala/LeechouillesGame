using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlienUI : MonoBehaviour
{
    public Slider jumpUI;
    public float jumpRefill = 1f;

    // Update is called once per frame
    void Update()
    {
        jumpUI.value = jumpRefill;
    }
}
