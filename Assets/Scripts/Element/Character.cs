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
    private void DisableInput()
    {
        canGetInput = false;
    }
    protected void Move(int Horizontal, int Vertical)
    {
        CanMoveTo(new Position(Horizontal, Vertical));
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
        bool CanMove = CanMoveTo(direction);
        return CanMove;
    }
    public void ApproachExit()
    {
        Board.CharacterApproachExit(this);
    }
}
