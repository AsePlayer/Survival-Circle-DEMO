using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(gameObject.tag != "CharacterSelect") return;
        
        PlayerInfo.playerInfo.SetShape("Square");
        Destroy(PlayerInfo.playerInfo.GetPlayer().gameObject);
    }
}
