using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vision : MonoBehaviour
{
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player")) {
      Fish.fishLeft = 3;
      SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reload scene
    }
  }
}
