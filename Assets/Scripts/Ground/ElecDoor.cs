using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class ElecDoor : Ground
{
    private bool DoorOpened = false;
    public List<ElecSwitch> ElecSwitches = new List<ElecSwitch>();
    public bool AllSwitchTouched = false;
    public void InitElecSwitches(string ElecSwitchScript)
    {
        if (string.IsNullOrEmpty(ElecSwitchScript)) return;
        var ElecSwitchScripts = ElecSwitchScript.Split(',');
        Regex regex = new Regex(@"\d");
        foreach(var Script in ElecSwitchScripts)
        {
            int yBegin = regex.Match(Script).Index;
            var yScript = Script.Substring(yBegin);
            var xScript = Script.Substring(0, yBegin);
            int.TryParse(yScript, out int y);
            var x = 0;
            if(yBegin == 1)
            {
                x = xScript[0] - 'A';
            }
            var elecGround = Board.GetGroundOfPosition(x, y);
            if(elecGround != null && elecGround is ElecSwitch elecSwitch)
            {
                ElecSwitches.Add(elecSwitch);
            }
        }
    }
    public override bool ThingCanMoveToMe(Element element)
    {
        if (!DoorOpened) return false;
        return true;
    }
    public void ElecSwitchTouched(ElecSwitch elecSwitch)
    {
        AllSwitchTouched = true;
        if (elecSwitch.Touched == true)
        {
            foreach(var es in ElecSwitches)
            {
                if (!es.Touched) AllSwitchTouched = false;
            }
        }
        if (AllSwitchTouched)
        {
            DoorOpen();
        }
        else
        {
            DoorClose();
        }
    }
    private void DoorOpen()
    {
        DoorOpened = true;
        Debug.Log("Door Opened");
    }
    private void DoorClose()
    {
        DoorOpened = false;
        Debug.Log("Door Closed");
    }
}
