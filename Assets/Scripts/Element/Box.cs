using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Element 
{
    void Start()
    {
        Type = ElementType.Box;
    }
    public override bool ThingCanMoveToMe(Element element, Position direction)
    {
        return CanMoveTo(direction);
    }
}
