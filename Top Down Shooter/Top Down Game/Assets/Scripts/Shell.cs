using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private float lifetime = 5; // in seconds

    private Material mat;
    private Color originalCol;
    private float fadePercent;
    private float deathTime;
    private bool fading;
    Renderer renderer_;
    Rigidbody rb;

    void Start()
    {
        renderer_ = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        mat = renderer_.material;
        originalCol = mat.color;
        deathTime = Time.time + lifetime;
        StartCoroutine("Fade");
    }

    IEnumerator Fade() { // Use Coroutine instead of update to help with performance
        while (true) {
            yield return new WaitForSeconds(.2f);
            if (fading) {
                fadePercent += Time.deltaTime;
                mat.color = Color.Lerp(originalCol, Color.clear, fadePercent);

                if (fadePercent >= 1) {
                    Destroy(gameObject);
                }
            }
            else {
                if (Time.time > deathTime) {
                    fading = true;
                }
            }
        }
    }
}
