using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreePatrol : MonoBehaviour
{
  public float speed = 5f; // move speed multiplier

  public List<Transform> points; // list of waypoints. Must not be child of body, otherwise they will move along it.
  private int index = 0;
  private Vector2 target; // current target
  private bool horizontal = true; // false -> vertical
  private int orientation = 0; // sprite orientation, to keep track of rotation: 
    // 0 = east
    // 1 = west
    // 2 = north
    // 3 = south

  void Start()
  {
    target = new Vector2(points[index].position.x, points[index].position.y);

    if (orientation == 1) {
      transform.localScale *= -1;
    }
    if (orientation == 2) {
      transform.Rotate(Vector3.forward * 90);
    }
    if (orientation == 3) {
      transform.Rotate(Vector3.forward * -90);
    }
  }

  void FixedUpdate() 
  {
    float step = speed * Time.deltaTime;
    transform.position = Vector2.MoveTowards(transform.position, target, step);

    if (horizontal) {
      if (transform.position.x == target.x) { // found
        index++;
        if (index >= points.Count) {
          index = 0;
        }
        target = new Vector2(points[index].position.x, points[index].position.y);

        if (transform.position.x == target.x) { // already at x, next point must be vertical
          horizontal = false;
          if (transform.position.y < target.y) { // target is north
            if (orientation == 0) { // was east
              transform.Rotate(Vector3.forward * 90);
            } 
            else { // was west
              transform.Rotate(Vector3.forward * -90);
            }
            orientation = 2; // now north
          }
          else { // target is south
            if (orientation == 0) { // was east
              transform.Rotate(Vector3.forward * -90);
            } 
            else { // was west
              transform.Rotate(Vector3.forward * 90);
            }
            orientation = 3; // now south
          }
        }
        else { // not at x, next point must be horizontal
          transform.localScale *= -1;
          if (orientation == 0) { // was east
            orientation = 1; // now west
          } 
          else { // was west
            orientation = 0; // now east
          }
        }
      }
    }
    else { // vertical 
      if (transform.position.y == target.y) { // found
        index++;
        if (index >= points.Count) {
          index = 0;
        }
        target = new Vector2(points[index].position.x, points[index].position.y);

        if (transform.position.y == target.y) { // already at y, next point must be horizontal
          horizontal = true;
          if (transform.position.x < target.x) { // target is east
            if (orientation == 2) { // was north
              transform.Rotate(Vector3.forward * -90);
            }
            else { // was south
              transform.Rotate(Vector3.forward * 90);
            }
            orientation = 0; // now east
          }
          else { // target is west
            if (orientation == 2) { // was north
              transform.Rotate(Vector3.forward * 90);
            }
            else { // was south
              transform.Rotate(Vector3.forward * -90);
            }
            orientation = 1; // now west
          }
        } 
        else { // not at y, next point must be vertical
          transform.Rotate(Vector3.forward * 180);
          if (orientation == 2) { // was north
            orientation = 3; // now south
          }
          else { // was south
            orientation = 2; // now north
          }
        }
      }
    }
  }
}
