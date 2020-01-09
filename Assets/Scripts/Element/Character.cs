using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Element
{
    private float MoveDelay = .3f, lastMoveT = 0f;
    private bool canGetInput = true;
    void Start()
    {
        Type = ElementType.Character;

        GameManager.Instance.OnLevelAccomplish += DisableInput;
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

    private bool IfGetInput()
    {
        bool GetInput = false;
        int Horizontal = (int)Input.GetAxisRaw("Horizontal");
        int Vertical = (int)Input.GetAxisRaw("Vertical");
        if((Horizontal != 0 || Vertical != 0) && !(Horizontal != 0 && Vertical != 0))
        {
            Move(Horizontal, Vertical);
            GetInput = true;
        }
        return GetInput;
    }
    private void DisableInput()
    {
        canGetInput = false;
    }
    private void Move(int Horizontal, int Vertical)
    {
        CanMoveTo(new Position(Horizontal, Vertical));
    }

    public override bool ThingCanMoveToMe(Element element, Position direction)
    {
        bool CanMove = CanMoveTo(direction);
        return CanMove;
    }

    public void ApproachExit()
    {

    }
}
