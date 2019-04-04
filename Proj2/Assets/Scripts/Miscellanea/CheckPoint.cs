using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint
{
    public int zone;
    public Vector2 position;

    public CheckPoint(int zone, Vector2 position)
    {
        this.zone = zone;
        this.position = position;
    }
}
