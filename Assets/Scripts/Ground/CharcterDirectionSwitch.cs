using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcterDirectionSwitch : SwitchBase
{
    public override bool ThingCanMoveToMe(Element element)
    {
        if (element is UnidirectionCharacter character) character.SwitchDirection();
        return true;
    }
}
