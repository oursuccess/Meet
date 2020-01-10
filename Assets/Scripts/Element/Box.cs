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
        bool CanMove = false;
        if (CanMoveTo(direction))
        {
            CanMove = true;
        }
        return CanMove;
    }

    public override void ThingMoveToMe(Element element, Position direction)
    {
        MoveTo(direction);
    }
}
