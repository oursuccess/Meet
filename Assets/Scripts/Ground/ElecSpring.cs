using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecSpring : ElecSwitch
{
    public override bool ThingCanMoveToMe(Element element)
    {
        element.OnMoving += DetermineTouching;
        return base.ThingCanMoveToMe(element);
    }
    private void DetermineTouching(Element element)
    {
        if(element.PositionInGrid.x == PositionInGrid.x && element.PositionInGrid.y == PositionInGrid.y)
        {
            ChangeTouchState(true);
        }
        else
        {
            ChangeTouchState(false);
            element.OnMoving -= DetermineTouching;
        }
    }
}
