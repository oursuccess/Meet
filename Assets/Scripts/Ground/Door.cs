using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : DoorBase
{
    private bool DoorOpened = false;
    public override bool ThingCanMoveToMe(Element element)
    {
        if (!DoorOpened)
        {
            var KeyTransform = element.gameObject.transform.Find("Key");
            if (KeyTransform != null)
            {
                DoorOpen();
                return true;
            }
            PlayCantMoveAudio();
            return false;
        }
        return true;
    }
    private void DoorOpen()
    {
        PlayDoorOpenAudio();
        gameObject.SetActive(false);
        DoorOpened = true;
    }
}
