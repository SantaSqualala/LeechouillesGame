using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HunterScanBehaviour : MonoBehaviour
{
    private SplitScreenInputHandler input;

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
        input = GetComponent<SplitScreenInputHandler>();
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
        CheckAliensInViewFrustrum();
        StartCoroutine(Scan(scanDuration));
        StartCoroutine(ScanReset(scanResetTimer));
    }

    // Scan for such duration
    private IEnumerator Scan(float duration)
    {
        foreach(GameObject alien in aliensInView)
        {
            alien.GetComponentInChildren<AlienPositionIndicatorBehaviour>().gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(duration);

        foreach (GameObject alien in aliensInView)
        {
            alien.GetComponentInChildren<AlienPositionIndicatorBehaviour>().gameObject.SetActive(false);
        }

        aliensInView.Clear();
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
    // Check for aliens in camera view
    private void CheckAliensInViewFrustrum()
    {
        foreach(AlienMovementBehaviour alien in FindObjectsOfType<AlienMovementBehaviour>())
        {
            if (isVisible(GetComponentInChildren<Camera>(), alien.gameObject))
            {
                aliensInView.Add(alien.gameObject);
            }
        }
    }

    // Check if specific gameobject is present on frustrum plane
    private bool isVisible(Camera c, GameObject target)
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
