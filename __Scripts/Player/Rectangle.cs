using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // tell PlayerInfo this is the square
        PlayerInfo.playerInfo.SetShape("Rectangle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
