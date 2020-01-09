using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Ground
{
    private bool DoorOpened = false;
    public override bool ThingCanMoveToMe(Element element)
    {
        if (!DoorOpened)
        {
            var KeyTransform = element.gameObject.transform.Find("Key");
            Debug.Log(KeyTransform);
            if (KeyTransform != null)
            {
                DoorOpen();
                return true;
            }
            return false;
        }
        return true;
    }
    private void DoorOpen()
    {
        DoorOpened = true;
        Debug.Log("DoorOpened");
    }
}
