using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    private int money;
    public Gun gun; 

    void Start() {
        money = 0;
        gun = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Gun>();
    }

    public void ModifyMoney(int m) {
        if (money+m < 0) {
            Debug.Log("attempting to have negative money");
        }
        else {
            money += m;
        }
    }

    public int GetMoney() {
        return money;
    }

    public override void Die() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
