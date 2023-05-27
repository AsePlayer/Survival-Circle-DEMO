using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player Information
    private Rigidbody2D rb;
    private int speed = 5;
    private bool canMove = true;
    private bool isAlive = true;

    LandController land;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // Get the Player rigidbody
        rb = GetComponent<Rigidbody2D>();

        // Get the land controller
        land = GameObject.FindGameObjectWithTag("Land").GetComponent<LandController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    // Check on collision for Enemy tag, if so, destroy player
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Enemy") {
            canMove = false; // Disable player movement
            isAlive = false; // Set player to dead
            rb.velocity = Vector2.zero; // Stop player movement

            // Shake player violently for a few seconds and then die
            StartCoroutine(ShakeAndDestroyCoroutine());
        }
    }

    // courotine to shake player violently for a few seconds
private IEnumerator ShakeAndDestroyCoroutine()
{
    // Get color of material emission map (for later use)
    Color emissionColor = spriteRenderer.material.GetColor("_EmissionColor");
        
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



    // Check if player is alive
    public bool IsAlive() {
        return isAlive;
    }
}
