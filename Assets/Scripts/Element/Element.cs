﻿using System.Collections;
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
    public bool CanMoveTo(Position direction)
    {
        if(Board.CanMoveTo(this, direction.x, direction.y))
        {
            transform.position += new Vector3(direction.x, direction.y);
            return true;
        }
        return false;
    }
    public virtual bool ThingCanMoveToMe(Element element, Position direction)
    {
        bool CanMove = false;
        return CanMove;
    }
    #endregion
    #region Grid
    public BoardManager Board;
    #endregion
}