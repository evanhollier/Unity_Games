using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int cost; // cost of gun
    public Gun.GunType targetGunType;
    public float targetFireRate;
    public float targetGunDamage;
    public int targetTotalAmmo;
    public int targetAmmoPerMag;
    public float targetReloadTime;
    public AudioClip targetShootSound;
    // public AudioClip targetReloadSound;
    

    AudioSource audio_;
    public AudioClip pickupSound;
    private Player player; 

    void Start() {
        audio_ = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update() {
        transform.Rotate(Vector3.up * 75.0f * Time.deltaTime);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            Pickup();
        }
    }

    void Pickup() {
        if (player.GetMoney() >= cost) {
            audio_.PlayOneShot(pickupSound);
            player.ModifyMoney(0-cost);

            player.gun.gunType = targetGunType;
            player.gun.fireRate = targetFireRate;
            player.gun.gunDamage = targetGunDamage;
            player.gun.currentAmmoInMag = targetAmmoPerMag;
            player.gun.totalAmmo = targetTotalAmmo;
            player.gun.ammoPerMag = targetAmmoPerMag;
            player.gun.reloadTime = targetReloadTime;
            player.gun.shootSound = targetShootSound;
            player.gun.secondsBetweenShots = 60/targetFireRate;
        }


    }
}
