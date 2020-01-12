using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCharacter : Character
{
    protected override void Start()
    {
        CanGetHorizontalInput = true;
        CanGetVerticalInput = true;

        base.Start();
    }
}
