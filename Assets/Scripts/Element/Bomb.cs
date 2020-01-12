using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Element
{
    private Character character;
    private int BombSteps = 1;
    private int CurrSteps = 0;
    void Start()
    {
        Type = ElementType.Bomb;
    }
    public override bool ThingCanMoveToMe(Element element, Position direction)
    {
        return CanMoveTo(direction);
    }
    public override void ThingMoveToMe(Element element, Position direction)
    {
        MoveTo(direction);
        if(element is Character character)
        {
            if (this.character == null) this.character = character;
            if (this.character != character)
            {
                this.character.OnMoving -= WaitToActivate;
                this.character = character;
            }
            if (this.character != null)
            {
                this.character.OnMoving += WaitToActivate;
            }
        }
    }
    private void WaitToActivate(Element element)
    {
        if(!Board.IsClosedTo(this, element))
        {
            CurrSteps++;
            if(CurrSteps != BombSteps)
            {
                element.OnMoving -= WaitToActivate;
                Boom();
            }
        }
        else
        {
            CurrSteps = 0;
        }
    }
    private void Boom()
    {
        int xBegin = PositionInGrid.x, yBegin = PositionInGrid.y;
        Element nextElement = null;
        nextElement = Board.GetElementOfPosition(xBegin + 1, yBegin);
        if (nextElement != null)
        {
            Board.RemoveElement(nextElement);
        }
        nextElement = Board.GetElementOfPosition(xBegin - 1, yBegin);
        if (nextElement != null)
        {
            Board.RemoveElement(nextElement);
        }
        nextElement = Board.GetElementOfPosition(xBegin, yBegin + 1);
        if (nextElement != null)
        {
            Board.RemoveElement(nextElement);
        }
        nextElement = Board.GetElementOfPosition(xBegin, yBegin - 1);
        if (nextElement != null)
        {
            Board.RemoveElement(nextElement);
        }
        Board.RemoveElement(this);
    }
}
