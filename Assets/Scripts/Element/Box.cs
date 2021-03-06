﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Element 
{
    protected override void Start()
    {
        Type = ElementType.Box;
        base.Start();
    }
    public override bool ThingCanMoveToMe(Element element, PositionInGrid direction)
    {
        bool CanMove = false;
        if (CanMoveTo(direction))
        {
            CanMove = true;
        }
        return CanMove;
    }

    public override void ThingMoveToMe(Element element, PositionInGrid direction)
    {
        MoveTo(direction);
    }
}
