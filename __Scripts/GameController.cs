using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    PlayerController player;
    // main camera
    Camera mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        // Cache Player
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();

        // Cache main camera
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Press R to restart the game
        if(Input.GetKeyDown(KeyCode.R)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        if(Input.GetKeyDown(KeyCode.G)) {
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
