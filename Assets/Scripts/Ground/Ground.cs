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
    #region Position
    public PositionInGrid PositionInGrid { get; protected set; }
    public void SetPosition(PositionInGrid position)
    {
        PositionInGrid = position;
    }

    #endregion
    public BoardManager Board;
    public virtual bool ThingCanMoveToMe(Element element)
    {
        return false;
    }
}
