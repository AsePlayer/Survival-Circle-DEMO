using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // tell PlayerInfo this is the rectangle
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(gameObject.tag != "CharacterSelect") return;

        PlayerInfo.playerInfo.SetShape("Rectangle");
        Destroy(PlayerInfo.playerInfo.GetPlayer().gameObject);
    }

}
