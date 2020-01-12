using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    #region Type
    public enum GroundType 
    {
        Exit,
        Floor,
        Wall,
        ElecDoor,
        ElecSwitch,
        Door,
    }
    public GroundType Type { get; protected set; }
    #endregion
    public BoardManager Board;
    public virtual bool ThingCanMoveToMe(Element element)
    {
        return false;
    }
}
