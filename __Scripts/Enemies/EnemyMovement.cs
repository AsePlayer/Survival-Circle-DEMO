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
    private SpriteRenderer spriteRenderer;

    private bool enteredScene = false;

    // ════════════════════════════
    //      Start and Update
    // ════════════════════════════
    private void Start()
    {
        // Get the game controller
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        // Get the land controller
        land = GameObject.FindGameObjectWithTag("Land").GetComponent<LandController>();

        // Get the sprite renderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Try to find the player controller
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();

        // Get the target position
        GetTargetPosition();
    }

    private void Update()
    {
        // if player is null, find him
        if(player == null) 
            player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();
            
        // Check if this enemy has entered the view of the camera
        CheckEnemyInView();

        // Move enemy towards the target position
        MoveTowardsTarget();

        // Check if the player is alive. If not, slow the enemy down
        CheckIfGameOver();
    }

    // ════════════════════════════
    //      Movement Methods
    // ════════════════════════════

    // Get the target position for the enemy to move towards
    private void GetTargetPosition()
    {
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

    // Move enemy towards the target position
    private void MoveTowardsTarget()
    {
        // Move enemy towards the movementDirection, with some randomness.
        transform.position += movementDirection * movementSpeed * Time.deltaTime;

        // Rotate enemy to face the movementDirection
        transform.rotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
    }

    // Check if the player is alive, if not, slow the enemy down
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

    // ════════════════════════════
    //        Death Methods
    // ════════════════════════════

    // Check if the enemy has entered the view of the camera (and left the scene)
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
            }
        }
        else
        {
            // Enemy is not in view and has left the scene
            if (enteredScene)
            {
                enteredScene = false;

                // Add score
                AddScore();
                
                // Wait for 1 second before destroying the enemy
                Invoke("DestroyEnemy", 1f);
            }
        }
    }

    // Destroy the enemy
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    // Add score to the UI
    private void AddScore() 
    {
        if(player.IsAlive())
        {
            gameController.ui.IncreaseScore(2);
        }
    }
    
}
