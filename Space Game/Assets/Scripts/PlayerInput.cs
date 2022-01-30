using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float forceMaginute;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float epsilon = 0.1f;
    [SerializeField] private float rotationSpeed;

    private Camera mainCamera;
    private Rigidbody rb;

    private Vector3 movementDirection;

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessInput();
        KeepOnScreen();
    }

    private void FixedUpdate() 
    {
        Move();
        Rotate();
    }

    private void ProcessInput()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

            movementDirection = transform.position - worldPosition;
            movementDirection.z = 0f;
            movementDirection.Normalize();
        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }

    private void KeepOnScreen()
    {
        Vector3 newPosition = transform.position;
        Vector3 viewPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (viewPosition.x > 1)
        {
            newPosition.x = -newPosition.x + epsilon;
        }
        else if (viewPosition.x < 0)
        {
            newPosition.x = -newPosition.x - epsilon;
        }

        if (viewPosition.y > 1)
        {
            newPosition.y = -newPosition.y + epsilon;
        }
        else if (viewPosition.y < 0)
        {
            newPosition.y = -newPosition.y - epsilon;
        }        

        transform.position = newPosition;
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
