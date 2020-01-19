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
    #region Start
    protected virtual void Start()
    {
    }
    #endregion
    #region Position
    public PositionInGrid PositionInGrid { get; protected set; }
    public void SetPosition(PositionInGrid position)
    {
        PositionInGrid = position;
    }
    public virtual bool ThingCanMoveToMe(Element element)
    {
        return false;
    }
    #endregion
    #region Board
    protected BoardManager Board;
    public void InitBoard(BoardManager boardManager)
    {
        Board = boardManager;
    }
    #endregion
}
