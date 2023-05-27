using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 5f; // Speed at which the enemy moves towards the middle
    public float movementAmplitude = 155f; // Amount of randomness in movement

    private Vector3 targetPosition;
    private Vector3 movementDirection;

    private LandController land;

    private void Start()
    {
        land = GameObject.FindGameObjectWithTag("Land").GetComponent<LandController>();
        targetPosition = land.GetCenterPosition(); // Set the target position to the middle
        movementDirection = (targetPosition - transform.position).normalized; // Set initial movement direction
        // add some randomness to the movement direction
        movementDirection += Random.insideUnitSphere / 2 * movementAmplitude;
    }

    private void Update()
    {
        // Move enemy towards the movementDirection, with some randomness.
        transform.position += movementDirection * movementSpeed * Time.deltaTime;
        // Rotate enemy to face the movementDirection
        transform.rotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
        
    }
}
