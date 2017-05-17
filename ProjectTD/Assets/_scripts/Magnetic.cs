using UnityEngine;
using System.Collections;

public class Magnetic : MonoBehaviour
{
    public LayerMask MagneticLayer;
    public Vector3 Position;
    public float Radius;
    public float Force;

    void FixedUpdate()
    {
        Collider[] colliders;
        Rigidbody rigidBody;
        colliders = Physics.OverlapSphere(transform.position + Position, Radius, MagneticLayer);
        foreach (Collider collider in colliders)
        {
            rigidBody = (Rigidbody)collider.gameObject.GetComponent(typeof(Rigidbody));
            if (rigidBody == null) continue;
            rigidBody.AddExplosionForce(Force * -1, transform.position + Position, Radius);
        }
    }
}
