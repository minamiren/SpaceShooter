using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpin : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 spin;
    public int spinSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        System.Random rnd = new System.Random();
        spin = new Vector3(rnd.Next(spinSpeed), rnd.Next(spinSpeed), rnd.Next(spinSpeed));

    }

    void FixedUpdate()
    {
        rb.angularVelocity = spin;
    }
}
