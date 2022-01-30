using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astreoid : MonoBehaviour
{
    [SerializeField] private float maxRotationSpeed = 1f;

    private void Start() 
    {
        GetComponent<Rigidbody>().angularVelocity = new Vector3 (0, 0, maxRotationSpeed * Random.Range(-1f, 1f));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
