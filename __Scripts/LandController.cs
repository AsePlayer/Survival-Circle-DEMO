using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandController : MonoBehaviour
{
    protected GameController gameController;
    
    // ════════════════════════════
    //           Start
    // ════════════════════════════

    // Start is called before the first frame update
    void Start()
    {
        // Cache game controller
        gameController = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<GameController>();
    }

    // ════════════════════════════
    //      Virtual Methods
    // ════════════════════════════

    // Returns the center position of the land. Will be overriden by child classes
    public virtual Vector2 GetCenterPosition() {
        print("LandController.GetCenterPosition");
        return Vector2.zero;
    }
}