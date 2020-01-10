using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Element : MonoBehaviour
{
    #region Type
    public enum ElementType
    {
        Box,
        Character,
    }
    public ElementType Type { get; protected set; }
    #endregion
    #region PositionInGrid
    public class Position
    {
        public int x;
        public int y;
        public Position(int x, int y) { this.x = x; this.y = y; }
    };
    public Position PositionInGrid { get; protected set; }
    public void SetPosition(Position position)
    {
        PositionInGrid = position;
    }
    #endregion
    #region MoveTo
    public bool CanMoveTo(Position direction)
    {
        return Board.CanMoveTo(this, direction.x, direction.y);
    }
    public void MoveTo(Position direction)
    {
        Board.ElementMoveTo(this, direction.x, direction.y);
        transform.position += new Vector3(direction.x, direction.y);
    }
    public virtual bool ThingCanMoveToMe(Element element, Position direction)
    {
        return false;
    }
    public abstract void ThingMoveToMe(Element element, Position direction);
    #endregion
    #region Grid
    public BoardManager Board;
    #endregion
    #region Chain
    public bool IsChained { get; protected set; } = false;
    public ChainChracter ChainedCharacter { get; protected set; } = null;
    public void ChainTo(ChainChracter chainChracter)
    {
        IsChained = true;
        ChainedCharacter = chainChracter;
    }
    public void UnChain()
    {
        IsChained = false;
        ChainedCharacter = null;
    }
    #endregion
}
