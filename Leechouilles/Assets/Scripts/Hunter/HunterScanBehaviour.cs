using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HunterScanBehaviour : MonoBehaviour
{
    private InputHandler input;

    [Header("Scan params")]
    [SerializeField] private float scanResetTimer = 45f;

    [Header("Scan UI")]
    [SerializeField] private RawImage scanIndicator;
    [SerializeField] private GameObject alienPosIndicator;
    private bool canScan = false;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<InputHandler>();
        StartCoroutine(ScanReset(5f));
    }

    // Update is called once per frame
    void Update()
    {
        if(canScan)
        {
            scanIndicator.color = Color.green;
        }
        else
        {
            scanIndicator.color = Color.red;
        }

        if(input.inputScan() && canScan)
        {
            Scan();
        }
    }

    #region Scan
    // Scan for aliens
    private void Scan()
    {
        foreach(AlienMovementBehaviour a in FindObjectsOfType<AlienMovementBehaviour>())
        {
            Instantiate(alienPosIndicator, a.transform.position, Quaternion.identity);
        }

        StartCoroutine(ScanReset(scanResetTimer));
    }

    // Reset scan capacity
    private IEnumerator ScanReset(float delay)
    {
        canScan = false;
        yield return new WaitForSeconds(delay);
        canScan = true;
    }
    #endregion
}
