using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectZoom : MonoBehaviour
{
    public SpriteRenderer land;

    // Use this for initialization
    void Start()
    {
        land = GameObject.FindGameObjectWithTag("Land").GetComponent<SpriteRenderer>();
        
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = land.bounds.size.x / land.bounds.size.y;

        if(screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = land.bounds.size.y / 2 + 2.75f;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = land.bounds.size.y / 2 * differenceInSize;
        }
    }
        
}
