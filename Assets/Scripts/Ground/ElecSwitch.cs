using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecSwitch : Ground
{
    private List<ElecDoor> ElecDoors;
    void Start()
    {
        InitDoor();
    }
    public void InitDoor()
    {
        ElecDoors = new List<ElecDoor>();
        var Objs = GameObject.FindGameObjectsWithTag("ElecDoor");
        foreach(var obj in Objs)
        {
            var ElecDoor = obj.GetComponent<ElecDoor>();
            if (ElecDoor)
            {
                ElecDoors.Add(ElecDoor);
            }
        }
    }
    public override bool ThingCanMoveToMe(Element element)
    {
        foreach(var ElecDoor in ElecDoors)
        {
            ElecDoor.DoorSwitch();
        }
        return true;
    }
}
