using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    // Cache player
    private PlayerController player;
    // Cache material
    private Material material;
    // Cache hitbox
    private Collider2D hitbox;

    // Cache game controller
    private GameController gameController;
    
    // ════════════════════════════
    //      Start and Update
    // ════════════════════════════
    
    // Start is called before the first frame update
    void Start()
    {
        // Cache game controller
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        // Cache player
        player = gameController.GetPlayer();

        // Set up Color Changer
        material = GetComponent<SpriteRenderer>().material;
        hitbox = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if mouse raycast hit this object
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse is over this object
            if (hitbox.OverlapPoint(mousePosition))
            {
                // Check if player is null
                if(player == null) 
                    player = gameController.GetPlayer();

                // Change the player color
                player.SetColor(material);

            }
        }

    }
}
