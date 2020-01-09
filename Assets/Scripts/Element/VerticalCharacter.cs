using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalCharacter : Character
{
    protected override bool IfGetInput()
    {
        bool GetInput = false;
        var y = (int)Input.GetAxisRaw("Vertical");
        if(y != 0)
        {
            Move(0, y);
            GetInput = true;
        }
        return GetInput;
    }
}
