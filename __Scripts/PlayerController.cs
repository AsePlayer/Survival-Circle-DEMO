using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player Information
    private Rigidbody2D rb;
    private int speed = 5;
    private bool canMove = true;

    LandController land;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        land = GameObject.FindGameObjectWithTag("Land").GetComponent<LandController>();
    }

    void Update()
    {
        if(!canMove) return;

        // Get player input
        Vector2 movement;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Move player
        rb.velocity = movement * speed;
        // Normalize the velocity
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);

        // Smoothly rotate player to face direction of movement based on velocity in 2D
        if (rb.velocity.magnitude > 0)
        {
            float targetAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(targetAngle - 90, Vector3.forward);
            float rotationSpeed = 360f; // Adjust this value to control the rotation speed
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Update player position
        transform.position = land.TryMovePosition(rb.position);
    }
}
