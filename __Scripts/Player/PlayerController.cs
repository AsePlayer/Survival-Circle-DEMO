using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ════════════════════════════
    //      Player Information
    // ════════════════════════════
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private TrailController trailController;
    private Color emissionColor;
    private int speed = 5;
    private bool canMove = true;
    private bool isAlive = true;

    // ════════════════════════════
    //      Game Information
    // ════════════════════════════
    private GameController gameController;
    private LandController land;
    private Collider2D boundary;

    // ════════════════════════════
    //      Start and Update
    // ════════════════════════════

    void Awake()
    {

    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Get the sprite renderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the trail controller
        trailController = GetComponentInChildren<TrailController>();

        // Get the land controller
        land = GameObject.FindGameObjectWithTag("Land").GetComponent<LandController>();
        boundary = land.GetComponent<Collider2D>();
        
        // Get the game controller
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        // Null guard the land
        if(land == null) 
        {
            land = GameObject.FindGameObjectWithTag("Land").GetComponent<LandController>();
            boundary = land.GetComponent<Collider2D>();
        }

        // Move player
        if (canMove)
            MovePlayer(GetPlayerMovement());
    }

    // ════════════════════════════
    //      Movement Methods
    // ════════════════════════════
    private Vector2 GetPlayerMovement() 
    {
        // Get the input axes
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement vector
        return new Vector2(horizontalInput, verticalInput);
    }

    // Move and rotate player within land boundary
    private void MovePlayer(Vector2 movement) 
    {
        // Move player and normalize the velocity
        rb.velocity = movement * speed;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);

        // Rotate player
        RotatePlayer();

        // Keep player in land
        KeepPlayerInLand();
    }

    // Keep player in land boundary
    private void KeepPlayerInLand() 
    {
        // Get the current position of the GameObject
        Vector2 currentPosition = transform.position;

        // Check if the current position is outside the land boundary
        if (!boundary.OverlapPoint(currentPosition))
        {
            // Find the closest point inside the boundary
            Vector2 closestPoint = boundary.ClosestPoint(currentPosition);

            // Move the GameObject to the closest point
            transform.position = closestPoint;
        }
    }

    // Rotate player to face direction of movement
    private void RotatePlayer()
    {
        // Smoothly rotate player to face direction of movement based on velocity in 2D
        if (rb.velocity.magnitude > 0)
        {
            // Calculate the target angle of rotation
            float targetAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;

            // Rotate the player by the target angle
            Quaternion targetRotation = Quaternion.AngleAxis(targetAngle - 90, Vector3.forward);

            // Rotate player
            float rotationSpeed = 360f; // Adjust this value to control the rotation speed
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    // ════════════════════════════
    //        Death Methods
    // ════════════════════════════

    // Check on collision for Enemy tag, if so, destroy player
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Enemy") {
            canMove = false; // Disable player movement
            isAlive = false; // Set player to dead

            Destroy(rb); // Destroy player rigidbody
            Destroy(other.gameObject.GetComponent<Rigidbody2D>());  // Destroy other rigidbody

            // Shake player violently for a few seconds and then die
            StartCoroutine(ShakeAndDestroyCoroutine());
        }
    }

    // Shake player violently for a few seconds and then die
    private IEnumerator ShakeAndDestroyCoroutine()
    {
        // Get color of material emission map (for later use)
        Color emissionColor = spriteRenderer.material.GetColor("_EmissionColor");

        // Put trail in front of player (looks better)
        trailController.DieOut();
            
        // Disable player collision
        GetComponent<Collider2D>().enabled = false;
        float shakeDuration = 1f;
        float shakeMagnitude = 0.2f;
        float destroyDelay = 1.5f;
        Vector3 initialPosition = transform.position;

        float elapsedTime = 0f;
        Vector3 originalPosition = initialPosition;

        while (elapsedTime < shakeDuration)
        {
            // Calculate the percent complete of the shake
            float percentComplete = elapsedTime / shakeDuration;
            float damper = 1f - Mathf.Clamp(4f * percentComplete - 3f, 0f, 1f);

            // Generate a random offset for the shake
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            float z = 0;
            Vector3 shakeOffset = new Vector3(x, y, z) * shakeMagnitude * damper;

            // Use Lerp to smoothly interpolate between original position and shaken position
            transform.position = Vector3.Lerp(originalPosition, originalPosition + shakeOffset, percentComplete);

            // Turn material emission map to grey over time
            spriteRenderer.material.SetColor("_EmissionColor", Color.Lerp(emissionColor, Color.black, percentComplete));

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        transform.position = originalPosition;
        
        yield return new WaitForSeconds(destroyDelay);
    }

    // ════════════════════════════
    //      Getter Methods
    // ════════════════════════════

    // Check if player is alive
    public bool IsAlive() {
        return isAlive;
    }

    public void SetColor(Material color) {
        spriteRenderer.material = color;
        PlayerInfo.playerInfo.SetMaterial(color);
    }

    public Material GetColor() {
        return spriteRenderer.material;
    }

    public SpriteRenderer GetSpriteRenderer() {
        return spriteRenderer;
    }
}
