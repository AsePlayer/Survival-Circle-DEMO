using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public UIController ui;
    PlayerController player;
    // main camera
    Camera mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        // Cache Player
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();

        // Cache UI
        ui = GetComponent<UIController>();

        // Cache main camera
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Add a score every 5 seconds
        if(Time.frameCount % (60 * 5) == 0 && player.IsAlive()) 
        {
            ui.IncreasePassiveScore(1);
        }

        if(player.IsAlive() == false) 
        {
            ui.ShowRestartButton();
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
    }

    public bool IsPlayerAlive() {
        return player.IsAlive();
    }

    public Camera GetMainCamera() {
        return mainCamera;
    }
    
}
