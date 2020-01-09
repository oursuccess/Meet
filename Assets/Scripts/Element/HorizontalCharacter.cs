using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCharacter : CharacterBase
{
    protected override bool IfGetInput()
    {
        bool GetInput = false;
        var x = (int)Input.GetAxisRaw("Horizontal");
        if(x != 0)
        {
            Move(x, 0);
            GetInput = true;
        }
        return GetInput;
    }
}
