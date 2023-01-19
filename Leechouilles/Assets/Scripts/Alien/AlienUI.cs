using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlienUI : MonoBehaviour
{
    public Image jumpUI;
    public TMP_Text lifeText;

    // Update is called once per frame
    void Update()
    {
        lifeText.text = " + ";
    }
}
