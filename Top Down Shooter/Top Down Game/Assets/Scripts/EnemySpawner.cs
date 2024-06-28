using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spawnee;
    public float spawnTime; // when to start spawning
    public float spawnDelay; // cooldown time
    public bool stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    public void SpawnObject() {
        if (!stopSpawning) {
            Instantiate(spawnee, transform.position, transform.rotation);
        }
    }

    // Stop spawning while player is inside collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            stopSpawning = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
            stopSpawning = false;
        }
    }
}
