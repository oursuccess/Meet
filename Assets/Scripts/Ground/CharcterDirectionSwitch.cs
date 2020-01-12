using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcterDirectionSwitch : Ground
{
    public override bool ThingCanMoveToMe(Element element)
    {
        if (element is UnidirectionCharacter character) character.SwitchDirection();
        return true;
    }
}
