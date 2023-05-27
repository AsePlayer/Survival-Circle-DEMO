using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 5f; // Speed at which the enemy moves towards the middle
    public float movementAmplitude = 15f; // Amount of randomness in movement

    private Vector3 targetPosition;
    private Vector3 movementDirection;

    private LandController land;
    private PlayerController player;

    private GameController gameController;

    private bool enteredScene = false;

    private void Start()
    {
        // Get the game controller
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        // Get the land controller
        land = GameObject.FindGameObjectWithTag("Land").GetComponent<LandController>();
        // Try to find the player controller
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();

        // Set the target position to the middle of the land
        targetPosition = land.GetCenterPosition(); 
        // Set initial movement direction
        movementDirection = (targetPosition - transform.position).normalized; 
        // Add some randomness to the movement direction
        movementDirection += Random.insideUnitSphere / 8 * movementAmplitude; 
        // Ensure movementSpeed is constant
        movementDirection.z = 0f;
        movementDirection = movementDirection.normalized;
    }

    private void Update()
    {
        // Move enemy towards the movementDirection, with some randomness.
        transform.position += movementDirection * movementSpeed * Time.deltaTime;

        // Rotate enemy to face the movementDirection
        transform.rotation = Quaternion.LookRotation(Vector3.forward, movementDirection);

        CheckIfGameOver();

        // Check if this enemy has entered the view of the camera
        CheckEnemyInView();

    }

    private void CheckIfGameOver() 
    {
        if(gameController.IsPlayerAlive() == false) 
        {
            // Update the movementSpeed to slow to a stop
            if(movementSpeed > 0) {
                movementSpeed -= movementSpeed / 200f;
            }
            else {
                movementSpeed = 0;
            }
        }
    }

    private void CheckEnemyInView()
    {
        // Raycast from the camera to the enemy's position
        Vector3 viewportPoint = gameController.GetMainCamera().WorldToViewportPoint(transform.position);
            
        if (viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1 && viewportPoint.z > 0)
        {
            // Enemy is in view
            if (!enteredScene)
            {
                enteredScene = true;
                Debug.Log("Enemy has entered the view");
            }
        }
        else
        {
            // Enemy is not in view and has left the scene
            if (enteredScene)
            {
                enteredScene = false;
                // Wait for 1 second before destroying the enemy
                Invoke("DestroyEnemy", 1f);
                    
                // TODO: Add score
            }
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    
}
