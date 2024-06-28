using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent  (typeof  (CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Handling variables
    public float rotationSpeed = 450; // 360 means player can rotate in 1 second
    public float walkSpeed = 5;
    public float runSpeed = 8;
    private float acceleration = 5;

    // System variables
    private Quaternion targetRotation;
    private Vector3 currentVelocityMod;

    // Components
    private CharacterController controller;
    private Camera cam;
    public Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ControlMouse();
        // ControlWASD();

        if (Input.GetButtonDown("Shoot")) {
            gun.Shoot();
        }
        else if (Input.GetButton("Shoot")) {
            gun.ShootContinuous();
        }

        if (Input.GetButtonDown("Reload")) {
            gun.TryReload();
        }
    }
    

    void ControlWASD() {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (input != Vector3.zero) {
            targetRotation = Quaternion.LookRotation(input);
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed*Time.deltaTime);
        }

        currentVelocityMod = Vector3.MoveTowards(currentVelocityMod, input, acceleration * Time.deltaTime);
        Vector3 motion = currentVelocityMod;
        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1)? .7f : 1; // account for diagonal movement being sqrt(2) times faster.
        motion *= (Input.GetButton("Run"))? runSpeed : walkSpeed;
        motion += Vector3.up * -8;

        controller.Move(motion * Time.deltaTime);
    }

    void ControlMouse() {
        Vector3 mousePos = Input.mousePosition;
        mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
        targetRotation = Quaternion.LookRotation(mousePos - new Vector3(transform.position.x, 0, transform.position.z));
        transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed*Time.deltaTime);

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        
        currentVelocityMod = Vector3.MoveTowards(currentVelocityMod, input, acceleration * Time.deltaTime);
        Vector3 motion = currentVelocityMod;
        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1)? .7f : 1; // account for diagonal movement being sqrt(2) times faster.
        motion *= (Input.GetButton("Run"))? runSpeed : walkSpeed;
        motion += Vector3.up * -8;

        controller.Move(motion * Time.deltaTime);
    }
}
