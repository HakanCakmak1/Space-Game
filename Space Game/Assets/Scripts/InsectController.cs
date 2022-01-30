using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectController : MonoBehaviour
{
    [SerializeField] private float forceMaginute;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float directionMultiplier;
    [SerializeField] private GameObject player;

    private Rigidbody rb;

    private Vector3 movementDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() 
    {
        Move();
        Rotate();
        SetInput();
    }

    private void SetInput()
    {
        if (player.activeSelf)
        {
            movementDirection = directionMultiplier * (player.transform.position - transform.position)  + movementDirection;
            movementDirection.z = 0f;
            movementDirection.Normalize();
        }
    }

    private void Rotate()
    {
        if (rb.velocity == Vector3.zero) { return; }
        Quaternion targetRotation = Quaternion.LookRotation(rb.velocity, Vector3.back);
        rb.rotation = Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed);
    }

    private void Move()
    {
        if (movementDirection == Vector3.zero) { return; }
        rb.AddForce(movementDirection * forceMaginute, ForceMode.Force);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
    }
}
