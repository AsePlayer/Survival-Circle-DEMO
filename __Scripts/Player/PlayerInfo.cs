using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    // Cache playercontroller
    private PlayerController player;

    private PlayerController daRealDeal;
    private Material playerMaterial;
    public Material defaultMaterial;

    private string shape;

    // list of prefabs
    public List<GameObject> prefabs;

    // Start is called before the first frame update

    public static PlayerInfo playerInfo;
    private void Awake()
    {
        if (playerInfo != null)
        {
            Destroy(gameObject);
            return;
        }

        playerInfo = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();
        
        // Initialize default material
        playerMaterial = defaultMaterial;

        if(player != null)
            playerMaterial = player.GetColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();

        if(player == null)
        {
            GameObject newPlayer;
            // if this is a Square
            if (shape == "Square")
            {
                // instantiate Square
                newPlayer = Instantiate(prefabs[0], new Vector3(0, 0, 0), Quaternion.identity);
                player = newPlayer.GetComponent<PlayerController>();
            }
            else if (shape == "Rectangle")
            {
                // instantiate Rectangle
                newPlayer = Instantiate(prefabs[1], new Vector3(0, 0, 0), Quaternion.identity);
                player = newPlayer.GetComponent<PlayerController>();
            }
            else 
            {
                // instantiate Square
                newPlayer = Instantiate(prefabs[0], new Vector3(0, 0, 0), Quaternion.identity);
                player = newPlayer.GetComponent<PlayerController>();
            }
        }
        if(player != null)
        {
            // try catch
            try
            {
                // if player is not dead
                if (player.IsAlive())
                    player.SetColor(playerMaterial);
            }
            catch
            {
                
            }
        }
    }

    public void SetMaterial(Material material)
    {
        playerMaterial = material;
    }

    public void SetShape(string shape)
    {
        this.shape = shape;
    }
}
