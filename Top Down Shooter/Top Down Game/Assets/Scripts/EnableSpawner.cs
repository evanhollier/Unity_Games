using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSpawner : MonoBehaviour
{
    // Enables attached spawner

    public EnemySpawner spawner;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            spawner.stopSpawning = false;
        }
    }
}
