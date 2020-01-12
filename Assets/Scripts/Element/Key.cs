using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Element
{
    public override bool ThingCanMoveToMe(Element element, PositionInGrid direction)
    {
        if(element is Character character)
        {
            return true;
        }
        return false;
    }
    public override void ThingMoveToMe(Element element, PositionInGrid direction)
    {
        if(element is Character character)
        {
            ThingHoldMe(character);
        }
    }
    private void ThingHoldMe(Character character)
    {
        var KeyObject = new GameObject("Key");
        KeyObject.transform.parent = character.gameObject.transform;
        KeyObject.transform.localPosition = new Vector3(0, 0);

        Board.RemoveElement(this);
    }
}
