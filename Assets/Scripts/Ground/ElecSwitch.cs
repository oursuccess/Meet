using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecSwitch : SwitchBase
{
    public delegate void ElementMoveToMeDel(ElecSwitch elecSwitch);
    public event ElementMoveToMeDel OnElementMoveToMe;
    public override bool ThingCanMoveToMe(Element element)
    {
        ChangeTouchState();
        OnElementMoveToMe?.Invoke(this);
        return true;
    }
}
