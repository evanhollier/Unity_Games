using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity
{

    public int moneyOnDeath; // money awarded when killed
    public float moveSpeed;
    private float baseMoveSpeed;
    private Player player; 
    private Rigidbody rb;
    private Collider coll;
    [HideInInspector] public Canvas canvas;
    private GameObject label;
    private Text healthText;
    private float hitCooldown = 0.5f; // Grace period for player continuously colliding with an enemy.

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        coll = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        label = new GameObject("HealthLabel");
        canvas = GameObject.FindGameObjectWithTag("EnemyCanvas").GetComponent<Canvas>();
        label.transform.SetParent(canvas.transform);
        healthText = label.AddComponent<Text>();
        healthText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        healthText.alignment = TextAnchor.MiddleCenter;

        baseMoveSpeed = moveSpeed;
        Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Enemy").GetComponent<Collider>(), coll); // ignore collision of other enemies
    }

    void Update() {
        transform.LookAt(player.transform.position);

        Vector3 labelPos = Camera.main.WorldToScreenPoint(this.transform.position);
        label.transform.position = labelPos;
        healthText.text = "" + this.health;
        healthText.horizontalOverflow = HorizontalWrapMode.Overflow;
    }
    void FixedUpdate() {
        rb.velocity = (transform.forward * moveSpeed);
    }

    public override void Die() 
    {
        player.ModifyMoney(moneyOnDeath);
        Destroy(label);
        base.Die();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            player.TakeDamage(1f);
            StartCoroutine(Knockback());
        }
    }
    IEnumerator Knockback()
    {
        moveSpeed = -10;
        yield return new WaitForSeconds(0.3f);
        moveSpeed = baseMoveSpeed; 
    }
    // Knockback is intended to prevent the player from continuously colliding with an enemy.
    // However, walls can allow this to happen. 
    // The following collision detectors make the player take rapid damage when continuously colliding with an enemy.
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) {
            hitCooldown -= Time.deltaTime;
            if ( hitCooldown < 0 ) {
                player.TakeDamage(1f);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
            hitCooldown = 0.5f;
        }
    }
}
