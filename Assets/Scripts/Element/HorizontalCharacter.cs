using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCharacter : UnidirectionCharacter 
{
    protected override void Start()
    {
        CanGetHorizontalInput = true;
        base.Start();
    }
}
