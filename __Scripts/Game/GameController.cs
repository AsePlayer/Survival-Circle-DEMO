using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    Camera mainCamera;
    PlayerController player;
    public GameObject background;
    public UIController ui;
    public LandController land;

    public int difficultyRamp;
    
    
    // ════════════════════════════
    //      Start and Update
    // ════════════════════════════

    // Start is called before the first frame update
        public static GameController Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
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
        
        StartCoroutine(IncreaseDifficulty());
    }

    // Update is called once per frame
    void Update()
    {
        // If player is null, try to find it
        if(player == null) 
            player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();

        if(player == null)
        {
            PlayerInfo.playerInfo.SpawnPlayer("Square");
        }

        // If land is null, try to find it
        if(land == null) 
            land = GameObject.FindGameObjectWithTag("Land").GetComponent<LandController>();

        if(IsPlayerAlive() == false) 
        {
            // wait a few seconds before showing the restart button
            Invoke("ShowRestartButton", 1.75f);
        }

        // Press R to restart the game
        if(Input.GetKeyDown(KeyCode.R)) 
        {
            SceneManager.LoadScene("Game");
        }

        // Press E to go to Main Menu
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            SceneManager.LoadScene("MainMenu");
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
        if(IsPlayerAlive() )
            background.transform.Rotate(0, 0, 0.005f + difficultyRamp * 0.0001f);
        else
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

    // Consistently increase difficulty
    IEnumerator IncreaseDifficulty()
    {
        if(player == null)
            yield return null;

        while (player.IsAlive())
        {
            yield return new WaitForSeconds(5f);

            // only increase speed if the state is in Game
            if (ui.GetScore() > 0)
            {
                difficultyRamp++;
                player.IncreaseSpeed(0.2f);
                ui.IncreasePassiveScore(1);
            }
        }
    }
}