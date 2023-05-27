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


    void Start()
    {
        // Find Boundary Collider object with layer "Land"
        boundaryCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Find the radius of the circular boundary
        movementRadiusX = boundaryCollider.bounds.extents.x;
        movementRadiusY = boundaryCollider.bounds.extents.y;

        // Get the center position
        centerPosition = boundaryCollider.bounds.center;

        // PingPong();
        // Squish();
    }

    public override Vector2 TryMovePosition(Vector2 newPos) 
    {
        // Check if new position is inside the circle
        float x = (newPos.x - centerPosition.x) / (movementRadiusX);
        float y = (newPos.y - centerPosition.y) / (movementRadiusY);

        // Perform check by using the equation of an oval (x/a)^2 + (y/b)^2 = 1, where a and b are the semi-major and semi-minor axes of the oval
        if (x * x + y * y > 1)
        {
            // If player is outside the oval, move them back to the edge of the oval
            float angle = Mathf.Atan2(y, x);
            // Calculate the angle of the point and move the player back to the edge of the oval by using the angle and the radius of the oval
            newPos = new Vector2(centerPosition.x + (movementRadiusX) * Mathf.Cos(angle), centerPosition.y + (movementRadiusY) * Mathf.Sin(angle));
        }

        // Update player position
        return newPos;
    }

    public override Vector2 GetCenterPosition() 
    {
        return centerPosition;
    }

    private void PingPong()
    {
        // Move left and right by 5 units every 2 seconds
        transform.position = new Vector2(Mathf.PingPong(Time.time * 2.5f, 10) - 5, transform.position.y);
    }

    private void Squish()
    {
        // Squish the land horizontally with time slowly
        transform.localScale = new Vector3(Mathf.PingPong(Time.time / 2, 1.5f) + 2.5f, transform.localScale.y, transform.localScale.z);
    }
}