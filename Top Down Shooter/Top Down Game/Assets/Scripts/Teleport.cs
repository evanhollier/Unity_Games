using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform target;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            StartCoroutine(tp(other));
        }
    }
    IEnumerator tp(Collider p)
    {
        yield return new WaitForSeconds(2);
        p.transform.position = target.position;
    }
}
