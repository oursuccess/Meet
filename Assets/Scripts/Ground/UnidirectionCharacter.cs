using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnidirectionCharacter : Character
{
    public virtual void SwitchDirection()
    {
        CanGetVerticalInput = !CanGetVerticalInput;
        CanGetHorizontalInput = !CanGetHorizontalInput;
    }
}
