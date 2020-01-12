using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : Element
{
    protected float MoveDelay = .3f, lastMoveT = 0f;
    protected bool canGetInput = true;
    public bool CanGetHorizontalInput { get; protected set; } = false;

    public bool CanGetVerticalInput { get; protected set; } = false;
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
        if(CanMoveTo(new PositionInGrid(Horizontal, Vertical)))
        {
            MoveTo(new PositionInGrid(Horizontal, Vertical));
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
    protected virtual bool IfGetInput()
    {
        bool GetInput = false;
        if (CanGetHorizontalInput)
        {
            var x = (int)Input.GetAxisRaw("Horizontal");
            if (x != 0)
            {
                Move(x, 0);
                GetInput = true;
            }
        }
        if(CanGetVerticalInput)
        {
            var y = (int)Input.GetAxisRaw("Vertical");
            if(y != 0)
            {
                Move(0, y);
                GetInput = true;
            }
        }
        return GetInput;
    }
    public override bool ThingCanMoveToMe(Element element, PositionInGrid direction)
    {
        bool CanMove = false;
        if (CanMoveTo(direction))
        {
            CanMove = true;
        }
        return CanMove;
    }
    public override void ThingMoveToMe(Element element, PositionInGrid direction)
    {
        MoveTo(direction);
    }
    public virtual void ApproachExit()
    {
        Board.CharacterApproachExit(this);
    }
}
