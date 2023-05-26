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
        
        // Player's new desired position
        Vector2 newPos = rb.position;

        // Update player position
        transform.position = land.TryMovePosition(newPos);
    }
}
