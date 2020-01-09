using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCharacter : Character
{

    protected override bool IfGetInput()
    {
        bool GetInput = false;
        int Horizontal = (int)Input.GetAxisRaw("Horizontal");
        int Vertical = (int)Input.GetAxisRaw("Vertical");
        if((Horizontal != 0 || Vertical != 0) && !(Horizontal != 0 && Vertical != 0))
        {
            Move(Horizontal, Vertical);
            GetInput = true;
        }
        return GetInput;
    }
}
