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
    #region MobileInput
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS
    private Vector2 TouchOrigin = -Vector2.one;
#endif
    #endregion
    protected Animator Animator;
    protected virtual void Start()
    {
        Type = ElementType.Character;
        Animator = gameObject.GetComponent<Animator>();

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
    public override void MoveTo(PositionInGrid direction)
    {
        Animator.SetTrigger("Move");
        base.MoveTo(direction);
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
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch PlayerTouch = Input.touches[0];
            if (PlayerTouch.phase == TouchPhase.Began)
            {
                TouchOrigin = PlayerTouch.position;
            }
            else if (PlayerTouch.phase == TouchPhase.Ended && TouchOrigin.x >= 0)
            {
                Vector2 TouchEnd = PlayerTouch.position;
                float x = TouchEnd.x - TouchOrigin.x;
                float y = TouchEnd.y - TouchOrigin.y;
                TouchOrigin.x = -1;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (CanGetHorizontalInput)
                    {
                        var horizontal = x > 0 ? 1 : -1;
                        Move(horizontal, 0);
                    }
                }
                else
                {
                    if (CanGetVerticalInput)
                    {
                        var vertical = y > 0 ? 1 : -1;
                        Move(0, vertical);
                    }
                }
            }
        }
#endif
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
