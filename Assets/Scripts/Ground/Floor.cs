using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : Ground
{
    void Start()
    {
        Type = GroundType.Floor;
    }
    public override bool ThingCanMoveToMe(Element element)
    {
        return true;
    }
}
