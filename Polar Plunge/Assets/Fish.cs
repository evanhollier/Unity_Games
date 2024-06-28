using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fish : MonoBehaviour
{
  [HideInInspector] public static int fishLeft = 3;

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player")) {
      fishLeft--;
      Destroy(gameObject);
      Debug.Log(fishLeft);
      if (fishLeft == 0) {
        fishLeft = 3;
        SceneManager.LoadScene(PlayerController.sceneIndex++);
      }
    }
  }
}
