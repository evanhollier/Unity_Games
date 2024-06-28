using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float speed = 5f; // move speed multiplier
  [HideInInspector] public static int sceneIndex = 2;
  

  void FixedUpdate()
  {
    // movement
    float step = speed * Time.deltaTime;
    transform.Translate(Input.GetAxisRaw("Horizontal") * step, Input.GetAxisRaw("Vertical") * step, 0);
  }
}
