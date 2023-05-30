using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // tell PlayerInfo this is the square
        PlayerInfo.playerInfo.SetShape("Square");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
