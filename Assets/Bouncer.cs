using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    int forceMultiplier = 4000;
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();

        Vector3 bounce = Vector3.Reflect(pc.lastVelocity.normalized, collision.contacts[0].normal);

        rb.AddForce(bounce * forceMultiplier);
    }
}
