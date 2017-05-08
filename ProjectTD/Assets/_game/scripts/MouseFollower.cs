using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MouseFollower : MonoBehaviour {

    [Header("Settings")]
    [Range(1, 10)]
    public float moveSpeed = 2;

    Rigidbody2D rb;
    Vector2 pos;
    Vector2 mousePos;
    Vector2 direction;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Parent's position
        pos = transform.position;
        // Target's position in world space
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Direction to target as a unit vector (scaled down to 1 so it can be multiplied)
        direction = (mousePos - pos).normalized;
        // Direction * speed * (per fraction of a second)
        rb.transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
