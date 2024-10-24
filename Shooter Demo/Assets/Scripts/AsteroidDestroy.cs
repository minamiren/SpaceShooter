using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider bolt)
    {
        if(bolt.tag == "Bolt")
        {
            Destroy(bolt.gameObject);
            Destroy(gameObject);
        }
    }
}
