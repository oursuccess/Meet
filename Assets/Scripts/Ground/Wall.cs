using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Ground
{
    void Start()
    {
        Type = GroundType.Floor;
    }
}
