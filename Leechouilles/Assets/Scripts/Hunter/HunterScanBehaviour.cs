using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HunterScanBehaviour : MonoBehaviour
{
    private InputHandler input;

    [Header("Scan params")]
    [SerializeField] private float scanDuration = 2.5f;
    [SerializeField] private float scanResetTimer = 45f;

    [Header("Scan UI")]
    [SerializeField] private Image scanIndicator;

    private List<GameObject> aliensInView = new List<GameObject>();
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

    #region Check view frustrum
    // Check if specific gameobject is present on frustrum plane
    private bool isGameObjectVisible(Camera c, GameObject target)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = target.transform.position;

        foreach(var plane in planes)
        {
            if(plane.GetDistanceToPoint(point) > 0)
            {
                return false;
            }
        }

        return true;
    }
    #endregion
}
