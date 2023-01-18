using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HunterShootBehaviour : MonoBehaviour
{
    private SplitScreenInputHandler input;
    private Camera cam;

    [Header("Shoot params")]
    [SerializeField] private float shootDelay = 1.3f;
    [SerializeField] private int munitions = 5;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform cannonTransform;
    private bool canShoot = false;

    [Header("Shooting UI & feedback")]
    [SerializeField] private Image fireIndicator;
    [SerializeField] private Image[] munitionUI;
    [SerializeField] private ParticleSystem shootSystem;
    [SerializeField] private AudioSource shootSound;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<SplitScreenInputHandler>();
        cam = GetComponentInChildren<Camera>();
        StartCoroutine(ShootReset(5f));
    }

    // Update is called once per frame
    void Update()
    {
        if(canShoot && munitions >= 0)
        {
            fireIndicator.color = Color.green;
        }
        else
        {
            fireIndicator.color = Color.red;
        }

        if (munitions <= 0)
        {
            Destroy(GetComponent<HunterMovementBehaviour>());
            Destroy(this);
        }

        if (input.inputFire() && canShoot)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // shoot bullet
        munitions--;
        GameObject bullet = Instantiate(bulletPrefab, cannonTransform);
        bullet.transform.SetParent(null, true);
        shootSound.Play();
        shootSystem.Play();

        // modify UI
        //munitionUI[munitions].color = new Color(0, 0, 0, 0);
        //Destroy(munitionUI[munitions]);

        StartCoroutine(ShootReset(shootDelay));
    }

    // Reset shoot capacity
    private IEnumerator ShootReset(float delay)
    {
        canShoot = false;
        yield return new WaitForSeconds(delay);
        canShoot = true;

        shootSound.Stop();
        shootSystem.Stop();
    }
}

