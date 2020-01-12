using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecDoor : Ground
{
    private bool DoorOpened = false;
    public override bool ThingCanMoveToMe(Element element)
    {
        if (!DoorOpened) return false;
        return true;
    }
    public void DoorSwitch()
    {
        DoorOpened = !DoorOpened;
        if (DoorOpened)
        {
            Debug.Log("Door Opened");
        }
        else
        {
            Debug.Log("Door Closed");
        }
    }
}
