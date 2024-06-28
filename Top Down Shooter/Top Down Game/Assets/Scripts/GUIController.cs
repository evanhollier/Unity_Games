using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{

    public Text MoneyText;
    public Text AmmoText;
    public Text HealthText;

    private Player player; 
    private PlayerController controller;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update() {
        MoneyText.text = "$" + player.GetMoney();
        AmmoText.text = controller.gun.currentAmmoInMag + " / " + controller.gun.totalAmmo;
        HealthText.text = "" + player.health;
    }
}
