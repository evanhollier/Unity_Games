using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class Gun : MonoBehaviour
{
    public LayerMask collisionMask;
    public enum GunType {Semi, Auto};
    public GunType gunType;
    public float fireRate; // in rounds-per-minute
    public float gunDamage; // damage inflicted on health
    public int totalAmmo; // ammunition in reserve 
    public int ammoPerMag; // ammunition in one magazine
    public float reloadTime; // seconds taken to reload the magazine
    public AudioClip shootSound;
    public AudioClip reloadSound;

    // Components
    public Transform spawnBullet;
    public Transform spawnShell; 
    public Rigidbody shell;
    private LineRenderer tracer;

    // System variables
    [HideInInspector] public float secondsBetweenShots;
    private float nextPossibleShootTime; 
    AudioSource audio_;
    [HideInInspector] public int currentAmmoInMag; 

    void Start() {
        audio_ = GetComponent<AudioSource>();
        secondsBetweenShots = 60/fireRate;
        if (GetComponent<LineRenderer>()) {
            tracer = GetComponent<LineRenderer>();
        }

        currentAmmoInMag = ammoPerMag;
    }

    public void Shoot()
    {
        if (CanShoot()) {
            Ray ray = new Ray(spawnBullet.position, spawnBullet.forward);
            RaycastHit hit;

            float shotDistance = 20;

            if (Physics.Raycast(ray, out hit, shotDistance, collisionMask)) {
                shotDistance = hit.distance;

                if (hit.collider.GetComponent<Entity>()) {
                    hit.collider.GetComponent<Entity>().TakeDamage(gunDamage);
                }
            }

            nextPossibleShootTime = Time.time + secondsBetweenShots;

            currentAmmoInMag--;

            audio_.PlayOneShot(shootSound);

            if (tracer) {
                StartCoroutine("RenderTracer", ray.direction * shotDistance);
            }

            Rigidbody newShell = Instantiate(shell, spawnShell.position, Quaternion.identity) as Rigidbody;

            newShell.AddForce(spawnShell.forward * Random.Range(150f,200f) + spawnBullet.forward * Random.Range(-10f,10f));
        }
    }
    public void ShootContinuous() {
        if (gunType == GunType.Auto) {
            Shoot();
        }
    }

    private bool CanShoot() {
        bool canShoot = true;

        if (Time.time < nextPossibleShootTime) {
            canShoot = false;
        }
        if (currentAmmoInMag == 0) {
            canShoot = false;
        }

        return canShoot;
    }

    public void TryReload() {
        if (totalAmmo != 0 && currentAmmoInMag != ammoPerMag) {
            Reload();
        }
    }
    public void Reload() {
        audio_.PlayOneShot(reloadSound);
        totalAmmo -= (ammoPerMag - currentAmmoInMag);
        currentAmmoInMag = ammoPerMag;
        if (totalAmmo < 0) {
            currentAmmoInMag += totalAmmo; // subtract missing ammo from magazine
            totalAmmo = 0;
        }
        nextPossibleShootTime += reloadTime;
    }


    IEnumerator RenderTracer(Vector3 hitPoint) {
        tracer.enabled = true;
        tracer.SetPosition(0, spawnBullet.position);
        tracer.SetPosition(1, spawnBullet.position + hitPoint);

        yield return null;
        tracer.enabled = false;
    }
}
