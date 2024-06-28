using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSpawner : MonoBehaviour
{
    // Disables attached spawner, unless it's turned back on bu entering and exiting.

    public EnemySpawner spawner;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            spawner.stopSpawning = true;
        }
    }
}
