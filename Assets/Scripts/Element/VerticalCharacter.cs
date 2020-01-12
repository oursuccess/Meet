using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalCharacter : UnidirectionCharacter
{
    protected override void Start()
    {
        CanGetVerticalInput = true;
        base.Start();
    }
}
