using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class ElecDoor : DoorBase
{
    #region Door
    private bool DoorOpened = false;
    public List<ElecSwitch> ElecSwitches = new List<ElecSwitch>();
    public bool AllSwitchTouched = false;
    private string ElecSwitchScript;
    public void InitElecSwitchScript(string Script)
    {
        ElecSwitchScript = Script;
        Board.OnBoardInitFinished += InitElecSwitches;
    }
    private void InitElecSwitches()
    {
        Board.OnBoardInitFinished -= InitElecSwitches;
        if (string.IsNullOrEmpty(ElecSwitchScript)) return;
        var ElecSwitchScripts = ElecSwitchScript.Split('+');
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
            var elecGround = Board.GetGroundOfPosition(x, y-1);
            if(elecGround != null && elecGround is ElecSwitch elecSwitch)
            {
                elecSwitch.OnElementMoveToMe += ElecSwitchTouched;
                ElecSwitches.Add(elecSwitch);
            }
        }
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
    #region Open
    private void DoorOpen()
    {
        PlayDoorOpenAudio();
        foreach(var es in ElecSwitches)
        {
            es.OnElementMoveToMe -= ElecSwitchTouched;
        }
        Destroy(gameObject);
        DoorOpened = true;
    }
    private void DoorClose()
    {
        DoorOpened = false;
    }
    #endregion
    #endregion
    #region Move
    public override bool ThingCanMoveToMe(Element element)
    {
        if (!DoorOpened)
        {
            PlayCantMoveAudio();
            return false;
        }
        return true;
    }
    #endregion
}
