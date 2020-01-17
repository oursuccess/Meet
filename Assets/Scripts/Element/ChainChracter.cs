using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainChracter : Character
{
    private List<Element> ChainedElements;
    private List<Element> CalculatedElements;
    private bool chaining = false;

    private float LastTouchT = 0.1f, TouchDelay = 0.1f;
    protected override bool IfGetInput()
    {
        bool GetInput = false;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchChain();
            GetInput = true;
        }
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch PlayerTouch = Input.touches[0];
            if(PlayerTouch.phase == TouchPhase.Began)
            {
                if(LastTouchT != 0f)
                {
                    LastTouchT = 0f;
                }
                else
                {
                    SwitchChain();
                }
            }
            else
            {
                LastTouchT += Time.deltaTime;
            }
            if(PlayerTouch.phase == TouchPhase.Ended && LastTouchT <= TouchDelay)
            {
                LastTouchT = 0f;
            }
        }
#endif
        var BaseGetInput = base.IfGetInput();
        return GetInput || BaseGetInput;
    }
    private void SwitchChain()
    {
        if (!chaining)
        {
            ChainAllClosedElements();
            chaining = true;
        }
        else
        {
                UnchainClosedElements();
                chaining = false;
        }
    }
    protected override void Move(int Horizontal, int Vertical)
    {
        var canMove = true;
        var direction = new PositionInGrid(Horizontal, Vertical);
        foreach(var element in ChainedElements)
        {
            if (!element.CanMoveTo(direction)) canMove = false;
        }
        if (canMove)
        {
            foreach(var element in ChainedElements)
            {
                var xPrev= element.PositionInGrid.x - Horizontal;
                var yPrev = element.PositionInGrid.y + Vertical;
                var prevElement = Board.GetElementOfPosition(xPrev, yPrev);
                //Debug.Log("Try Moving Element: " + element + " PositionX: " + element.PositionInGrid.x + " y: " + element.PositionInGrid.y);
                //Debug.Log("xPrev: " + xPrev + " yPrev: " + yPrev + " prevElement: " + prevElement);
                if(!ChainedElements.Contains(prevElement))
                {
                    //Debug.Log("moving " + element);
                    element.MoveTo(direction);
                }
            }
        }
    }
    private void ChainAllClosedElements()
    {
        ChainedElements = new List<Element>();
        CalculatedElements = new List<Element>();
        ChainedElements.Add(this);
        CalculateClosedOfElement(this);
    }
    private void CalculateClosedOfElement(Element element)
    {
        if (!CalculatedElements.Contains(element))
        {
            CalculatedElements.Add(element);
            int xBegin = element.PositionInGrid.x, yBegin = element.PositionInGrid.y;
            Element nextElement = null;
            nextElement = Board.GetElementOfPosition(xBegin + 1, yBegin);
            if (nextElement != null) 
            { 
                AddElementToChainedElements(nextElement);
                CalculateClosedOfElement(nextElement);
            }
            nextElement = Board.GetElementOfPosition(xBegin - 1, yBegin);
            if (nextElement != null) 
            {
                AddElementToChainedElements(nextElement); 
                CalculateClosedOfElement(nextElement);
            }
            nextElement = Board.GetElementOfPosition(xBegin, yBegin + 1);
            if (nextElement != null) 
            { 
                AddElementToChainedElements(nextElement); 
                CalculateClosedOfElement(nextElement);
            }
            nextElement = Board.GetElementOfPosition(xBegin, yBegin - 1);
            if (nextElement != null) 
            {
                AddElementToChainedElements(nextElement);
                CalculateClosedOfElement(nextElement);
            }
        }
    }
    private void AddElementToChainedElements(Element element)
    {
        if (!ChainedElements.Contains(element))
        {
            ChainedElements.Add(element);
            element.ChainTo(this);
            if (element is Character character)
            {
                if (element != this)
                {
                    character.DisableInput();
                }
                if (character is NormalCharacter normalCharacter)
                {
                    CanGetVerticalInput = true;
                    CanGetHorizontalInput = true;
                }
                else if(character is VerticalCharacter verticalCharacter)
                {
                    CanGetVerticalInput = true;
                }
                else if(character is HorizontalCharacter horizontalCharacter)
                {
                    CanGetHorizontalInput = true;
                }
            }
        }
    }
    private void UnchainClosedElements()
    {
        foreach(var element in ChainedElements)
        {
            element.UnChain();
            if(element is Character character && element!= this)
            {
                character.EnableInput();
            }
        }
        ChainedElements.Clear();
        CanGetHorizontalInput = false;
        CanGetVerticalInput = false;
    }

    public override void ApproachExit()
    {
        if(ChainedElements != null || ChainedElements.Count != 0)
        {
            foreach(var element in ChainedElements)
            {
                if(element != this && element is Character character)
                {
                    character.UnChain();
                    character.ApproachExit();
                }
                else
                {
                    element.UnChain();
                    Destroy(element.gameObject);
                }
            }
        }
        base.ApproachExit();
    }
}
