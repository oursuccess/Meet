using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : Element
{
    protected float MoveDelay = .3f, lastMoveT = 0f;
    protected bool canGetInput = true;
    protected virtual void Start()
    {
        Type = ElementType.Character;

        GameManager.Instance.OnLevelAccomplish += DisableInput;
    }
    public void DisableInput()
    {
        canGetInput = false;
    }
    public void EnableInput()
    {
        canGetInput = true;
    }
    protected virtual void Move(int Horizontal, int Vertical)
    {
        if(CanMoveTo(new Position(Horizontal, Vertical)))
        {
            MoveTo(new Position(Horizontal, Vertical));
        }
    }
    void Update()
    {
        if(lastMoveT < MoveDelay)
        {
            lastMoveT += Time.deltaTime;
        }
        else
        {
            if (canGetInput && IfGetInput())
            {
                lastMoveT = 0f;
            }
        }
    }
    protected abstract bool IfGetInput();
    public override bool ThingCanMoveToMe(Element element, Position direction)
    {
        bool CanMove = false;
        if (CanMoveTo(direction))
        {
            CanMove = true;
        }
        return CanMove;
    }
    public override void ThingMoveToMe(Element element, Position direction)
    {
        MoveTo(direction);
    }
    public void ApproachExit()
    {
        Board.CharacterApproachExit(this);
    }
}
