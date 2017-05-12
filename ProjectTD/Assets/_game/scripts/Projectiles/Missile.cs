using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Missile : MonoBehaviour
{
    public Transform Target;
    public float flightSpeed = 2f;
    float rotSpeed = 2.5f;
    float lastRot= 0f;

    private Rigidbody2D rb;
    Quaternion newRot;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Invoke("Explode", 5f);
    }

    void FixedUpdate()
    {
        Debug.DrawLine(transform.position, Target.position, Color.yellow);

        newRot = Quaternion.LookRotation(transform.position - Target.position, Vector3.forward);
        newRot.x = newRot.y = 0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * rotSpeed);
        rb.velocity = transform.up * flightSpeed;

        if(rotSpeed < 40f)
        {
            lastRot += Time.deltaTime * Time.deltaTime * 50f;
            rotSpeed += lastRot;
        }
    }

    private void Explode()
    {
        CancelInvoke("Explode");
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Explode();
        }
    }
}
