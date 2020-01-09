using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Element
{
    public override bool ThingCanMoveToMe(Element element, Position direction)
    {
        if(element is Character character)
        {
            ThingHoldMe(character);
            return true;
        }
        return false;
    }

    private void ThingHoldMe(Character character)
    {
        var KeyObject = new GameObject("Key");
        KeyObject.transform.parent = character.gameObject.transform;
        KeyObject.transform.localPosition = new Vector3(0, 0);

        Board.RemoveElement(this);
    }
}
