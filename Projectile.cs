using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private float delay = 4.0f;
    public float speed = 25.0f;
    Rigidbody projectileRb;

    // Start is called before the first frame update
    void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
        Destroy(gameObject, delay);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
