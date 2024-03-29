using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeController : MonoBehaviour
{
    // Cache ColorChanger Prefab
    public GameObject colorSelectorPrefab;

    // List of materials
    public List<Material> materials;

    // Parent object for the colorSelectorPrefabs
    private GameObject colorSelectorParent;

    // Menu Canvas
    public GameObject menuCanvas;

    public StyleButton styleButton;

    GameObject[] characterSelects;

    // ════════════════════════════
    //      Start and Update
    // ════════════════════════════

    // Start is called before the first frame update
    void Start()
    {        
        // Spawn a parent object for the colorSelectorPrefabs
        colorSelectorParent = new GameObject("Color Selector Parent");

        // Spawn as many colorSelectorPrefab as there are materials in a 7x3 grid
        for (int i = 0; i < materials.Count; i++)
        {
            // Calculate the position of the colorSelectorPrefab
            int row = i / 7; // Calculate the row index
            int col = i % 7; // Calculate the column index

            // Calculate the position based on row and col
            float x = col - 3f;
            float y = 1f - row;

            // Spawn the colorSelectorPrefab
            GameObject colorSelector = Instantiate(colorSelectorPrefab, new Vector2(x, y), Quaternion.identity);

            // Set the material of the colorSelectorPrefab
            colorSelector.GetComponent<SpriteRenderer>().material = materials[i];

            // Set the parent of the colorSelectorPrefab
            colorSelector.transform.parent = colorSelectorParent.transform;

            // Disable parent until the player overlaps with the StyleButton
            
        }

        // spawn the Player Info prefabs list
        for(int i = 0; i < PlayerInfo.playerInfo.prefabs.Count; i++)
        {
            // Calculate the position of the colorSelectorPrefab
            int row = i / 7; // Calculate the row index
            int col = i % 7; // Calculate the column index

            // Calculate the position based on row and col
            float x = col - 1f;
            float y = 3f - row;

            // Spawn the colorSelectorPrefab
            GameObject colorSelector = Instantiate(PlayerInfo.playerInfo.prefabs[i], new Vector2(x * 2, y), Quaternion.identity);
            colorSelector.GetComponent<PlayerController>().enabled = false;
            // change tag to "Default"
            colorSelector.tag = "CharacterSelect";
            
            // Set the parent of the colorSelectorPrefab
            colorSelector.transform.parent = colorSelectorParent.transform;

        }

        characterSelects = GameObject.FindGameObjectsWithTag("CharacterSelect");

        // Move to the right position
        colorSelectorParent.transform.position = new Vector2(13.5f, 0f);
        colorSelectorParent.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject characterSelect in characterSelects)
        {
            characterSelect.transform.Rotate(0, 0, -0.1f);
        }
    }

    // Get parent object of the colorSelectorPrefabs
    public GameObject GetColorSelectorParent()
    {
        return colorSelectorParent;
    }
}
