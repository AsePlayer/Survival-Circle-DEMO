using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;
    private Color emissionColor;
    private bool isAlive = true;

    // ════════════════════════════
    //      Start and Update
    // ════════════════════════════

    // Start is called before the first frame update
    void Start()
    {
        // Get the sprite renderer from the parent
        spriteRenderer = GetComponentInParent<SpriteRenderer>();

        // Get the trail renderer
        trailRenderer = GetComponent<TrailRenderer>();

        // Set trail renderer color to emission color
        emissionColor = spriteRenderer.material.GetColor("_EmissionColor");
        trailRenderer.startColor = emissionColor;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive) CheckIfColorChanged();
    }

    // ════════════════════════════
    //      Trail Methods
    // ════════════════════════════

    // Change the trail color if the player changes color
    void CheckIfColorChanged()
    {
        if (emissionColor != spriteRenderer.material.GetColor("_EmissionColor"))
        {
            emissionColor = spriteRenderer.material.GetColor("_EmissionColor");
            trailRenderer.startColor = emissionColor;
        }
    }

    // Place the trail on top of the player when they die, it looks cooler 
    public void DieOut()
    {
        isAlive = false;
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }
}