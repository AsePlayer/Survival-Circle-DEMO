using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    // Cache game controller
    private GameController gameController;

    // Cache CustomizeController
    private CustomizeController customizeController;

    // Cache player from game controller
    private PlayerController player;

    private LandController land;

    // bool that tracks if player has chosen Style
    private bool isOverlapComplete = false; // Variable to track if overlap check is already completed

    private bool stoppedOverlapping = true; // Variable to track if player has stopped overlapping

    
    // ════════════════════════════
    //      Start and Update
    // ════════════════════════════
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        customizeController = GameObject.FindGameObjectWithTag("CustomizeController").GetComponent<CustomizeController>();
        player = gameController.GetPlayer();

        land = gameController.land;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null) 
            player = gameController.GetPlayer();

        if(land == null) 
            land = gameController.land;

        if(isOverlapComplete)
        {
            // time to style
            customizeController.GetColorSelectorParent().SetActive(true);
            
            // hide menu
            customizeController.menuCanvas.transform.GetChild(0).gameObject.SetActive(false);

            // show back button
            customizeController.menuCanvas.transform.GetChild(1).gameObject.SetActive(true);

            // translate land to the left with lerp
            land.transform.position = Vector3.Lerp(land.transform.position, new Vector3(-4f, 0f, 15f), 0.1f);

            Bounds bounds = land.GetComponent<Collider2D>().bounds;
            Vector3 bottomLeftPosition = new Vector3(bounds.min.x * 1f, bounds.min.y * 0.6f, 0f);
            customizeController.menuCanvas.transform.GetChild(1).transform.position = Vector2.Lerp(customizeController.menuCanvas.transform.GetChild(1).transform.position, bottomLeftPosition, 0.1f);
            customizeController.GetColorSelectorParent().transform.position = Vector3.Lerp(customizeController.transform.position, land.transform.position + new Vector3(-7f, 0f, 0), 0.1f);
        }
    }

private void OnTriggerStay2D(Collider2D other)
{
    if (other.gameObject.CompareTag("Player"))
    {
        stoppedOverlapping = false; // Set overlap check completion flag to false

        // Check if the player is null
        if (player == null)
        {
            player = gameController.GetPlayer();
        }
        else if (!isOverlapComplete) // Check if overlap check is not already completed
        {
            StartCoroutine(CheckOverlapDuration());
        }
    }
}

private void OnTriggerExit2D(Collider2D other)
{
    if (other.gameObject.CompareTag("Player"))
    {
        stoppedOverlapping = true; // Set overlap check completion flag to true
    }
}

// Coroutine to check the overlap duration
private IEnumerator CheckOverlapDuration()
{
    float overlapDuration = 0f;
    isOverlapComplete = false; // Reset overlap check completion flag

    while (overlapDuration < 1f)
    {
        overlapDuration += Time.deltaTime;
        yield return null;
    }

    if (!isOverlapComplete && !stoppedOverlapping)
    {
        isOverlapComplete = true;
        Debug.Log(gameObject.name);
    }
}
}
