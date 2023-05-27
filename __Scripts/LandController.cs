using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandController : MonoBehaviour
{

    // Checks if player is inside the land. Will be overriden by child classes
    public virtual Vector2 TryMovePosition(Vector2 newPos) {
        print("LandController.TryMovePosition");
        return newPos;
    }

    public virtual Vector2 GetCenterPosition() {
        print("LandController.GetCenterPosition");
        return Vector2.zero;
    }
}
