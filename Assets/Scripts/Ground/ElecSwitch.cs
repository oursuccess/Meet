using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecSwitch : Ground
{
    public delegate void ElementMoveToMeDel(ElecSwitch elecSwitch);
    public event ElementMoveToMeDel OnElementMoveToMe;
    public bool Touched { get; protected set; } = false;
    public override bool ThingCanMoveToMe(Element element)
    {
        Touched = !Touched;
        OnElementMoveToMe?.Invoke(this);
        return true;
    }
}
