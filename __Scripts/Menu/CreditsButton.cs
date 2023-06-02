using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{

    // Cache game controller
    private GameController gameController;

    // Cache player from game controller
    private PlayerController player;

    private LandController land;

    // bool that tracks if player has chosen Style
    public bool isOverlapComplete = false; // Variable to track if overlap check is already completed
    public bool stoppedOverlapping = true; // Variable to track if player has stopped overlapping

    // Store tmpro made by me
    public TMPro.TextMeshProUGUI creditsText;
    public TMPro.TextMeshProUGUI creditsNameText;

    // store initial position of credits text
    private Vector3 initialPosition;
    
    // ════════════════════════════
    //      Start and Update
    // ════════════════════════════
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        player = gameController.GetPlayer();

        land = gameController.land;

        initialPosition = creditsNameText.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            player = gameController.GetPlayer();

        if (land == null)
            land = gameController.land;

        // Check if mouse clicked (functionally the same as the overlap check)
        CheckForMouseClick();

        if (IsOverlapComplete())
        {
            // Change MenuManager State
            MenuManager.Instance.UpdateMenuState(MenuManager.MenuState.Credits);

            // Perform actions when overlap is complete
            DisplayColors();
            PerformUIAdjustments();
            MoveLandToUp();
            MoveMenuAndColorSelector();

            // IEnumerator to set isOverlapComplete to false after 1 second
            StartCoroutine(ResetOverlapComplete());
        }

    }

    bool IsOverlapComplete()
    {
        return isOverlapComplete;
    }

    void DisplayColors()
    {
        // Time to style
        // customizeController.GetColorSelectorParent().SetActive(true);
        creditsNameText.enabled = true;
        creditsNameText.transform.position = Vector3.Lerp(land.transform.position, new Vector3(0f, land.GetCenterPosition().y - 55f, 15f), 0.1f);
        creditsText.enabled = false;


    }

    void PerformUIAdjustments()
    {
        // // Hide menu
        // customizeController.menuCanvas.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().enabled = false;

        // // Show back button
        // // transform.root.transform.Find("BackButton").gameObject.SetActive(true);
        // customizeController.menuCanvas.transform.GetChild(0).gameObject.SetActive(true);
    }

    void MoveLandToUp()
    {
        // Translate land to the left with lerp
        land.transform.position = Vector3.Lerp(land.transform.position, new Vector3(0f, 2f, 15f), 0.1f);
    }

    void MoveMenuAndColorSelector()
    {
        // // Move color selector parent
        // customizeController.GetColorSelectorParent().transform.position = Vector3.Lerp(customizeController.transform.position, land.transform.position + new Vector3(-7f, 0f, 0), 0.1f);
    }

    void PerformReverseActions()
    {
        // // Reverse actions when overlap is not complete
        // customizeController.GetColorSelectorParent().SetActive(false);
        // customizeController.menuCanvas.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
        // customizeController.menuCanvas.transform.GetChild(0).gameObject.SetActive(false);
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

    private IEnumerator ResetOverlapComplete()
    {
        yield return new WaitForSeconds(1f);
        isOverlapComplete = false;

    }

    private void CheckForMouseClick()
    {
        // Check if mouse clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse is over this object
            if (GetComponent<Collider2D>().OverlapPoint(mousePosition))
            {
                isOverlapComplete = true;
            }
        }
    }

}