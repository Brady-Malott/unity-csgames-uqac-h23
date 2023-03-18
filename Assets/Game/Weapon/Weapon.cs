using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{

    public int currentAmmo = 12;
    public int maxAmmo = 12;
    public bool isReloading = false;
    public float range = 25f;
    public Camera fpscam;
    public ParticleSystem collisionEffect;
    public TextMeshProUGUI ammoText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update ammo count UI text
        ammoText.text = isReloading ? "Reloading..." : "Ammo: " + currentAmmo + " / " + maxAmmo;

        if (isReloading) return;

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            RaycastHit hit;

            if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, range))
            {
                Debug.Log("Hit Zain");
                Instantiate(collisionEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }

            if (currentAmmo == 0)
            {
                StartCoroutine(Reload());
            }
        } else
        {
            Debug.Log("Did not hit Zain");
        }
        
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading");
        yield return new WaitForSeconds(3);
        currentAmmo = maxAmmo;
        Debug.Log("Reloaded");
        isReloading = false;
    }
}
