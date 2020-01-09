using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Ground
{
    void Start()
    {
        Type = GroundType.Exit;
    }
    public override bool ThingCanMoveToMe(Element element)
    {
        if(element.Type == Element.ElementType.Character)
        {
            if(element is CharacterBase character)
            {
                character.ApproachExit();
            }
        }
        return true;
    }
}
