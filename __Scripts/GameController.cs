using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Camera mainCamera;
    PlayerController player;
    public GameObject background;
    public UIController ui;
    public LandController land;
    
    // ════════════════════════════
    //      Start and Update
    // ════════════════════════════

    // Start is called before the first frame update
    void Start()
    {
        // Cache Player
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();

        // Cache background
        background = GameObject.FindGameObjectWithTag("Background");

        // Cache UI
        ui = GetComponent<UIController>();

        // Cache main camera
        mainCamera = Camera.main;

        // Cache land
        land = GameObject.FindGameObjectWithTag("Land").GetComponent<LandController>();
    }

    // Update is called once per frame
    void Update()
    {
        // If player is null, try to find it
        if(player == null) 
            player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();

        // If land is null, try to find it
        if(land == null) 
            land = GameObject.FindGameObjectWithTag("Land").GetComponent<LandController>();

        // Add a score every 5 seconds
        if(Time.frameCount % (60 * 5) == 0 && IsPlayerAlive()) 
        {
            ui.IncreasePassiveScore(1);
        }

        if(IsPlayerAlive() == false) 
        {
            // wait a few seconds before showing the restart button
            Invoke("ShowRestartButton", 1.75f);
        }

        // Press R to restart the game
        if(Input.GetKeyDown(KeyCode.R)) 
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        // Press G to get GODMODE
        if(Input.GetKeyDown(KeyCode.G)) 
        {
            // Disable player collision
            player.GetComponent<Collider2D>().enabled = !player.GetComponent<Collider2D>().enabled;
        }

        // Exit the game with ESC
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }

        // Rotate the background smoothly
        background.transform.Rotate(0, 0, 0.005f);
    }

    // ════════════════════════════
    //      Helper Functions
    // ════════════════════════════

    public bool IsPlayerAlive() {
        // Null guard the player
        if(player == null) return false;
        return player.IsAlive();
    }

    public PlayerController GetPlayer() {
        return player;
    }

    public Camera GetMainCamera() {
        return mainCamera;
    }

    private void ShowRestartButton() {
        ui.ShowRestartButton();
    }
}