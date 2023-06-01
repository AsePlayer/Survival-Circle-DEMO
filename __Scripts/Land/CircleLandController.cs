using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLandController : LandController
{
    // Land Information (Boundary Collider)
    private Collider2D boundaryCollider;
    private Vector2 centerPosition;
    float movementRadiusX;
    float movementRadiusY;

    // ════════════════════════════
    //      Start and Update
    // ════════════════════════════
    void Start()
    {
        // Find Boundary Collider object with layer "Land"
        boundaryCollider = GetComponent<Collider2D>();

        // Cache game controller
        gameController = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<GameController>();
    }

    void Update()
    {
        // Find the radius of the circular boundary
        movementRadiusX = boundaryCollider.bounds.extents.x;
        movementRadiusY = boundaryCollider.bounds.extents.y;

        // Get the center position
        centerPosition = boundaryCollider.bounds.center;

        // Rotate the land smoothly if the player is alive
        if(gameController.IsPlayerAlive()) transform.Rotate(0, 0, -0.01f * (2 + gameController.difficultyRamp));
        else transform.Rotate(0, 0, -0.01f); 
        
    }

    // ════════════════════════════
    //      Change Land Shape
    // ════════════════════════════

    // Move land left and right
    private void PingPong()
    {
        // Move left and right by 5 units every 2 seconds
        transform.position = new Vector2(Mathf.PingPong(Time.time * 2.5f, 10) - 5, transform.position.y);
    }

    // Squish land horizontally
    private void Squish()
    {
        // Squish the land horizontally with time slowly
        transform.localScale = new Vector3(Mathf.PingPong(Time.time / 2, 1.5f) + 2.5f, transform.localScale.y, transform.localScale.z);
    }

    // ════════════════════════════
    //      Getters Functions
    // ════════════════════════════
    public override Vector2 GetCenterPosition() 
    {
        return centerPosition;
    }

    public Collider2D GetBoundaryCollider() 
    {
        return boundaryCollider;
    }
}