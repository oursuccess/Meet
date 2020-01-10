using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainChracter : Character
{
    private List<Element> ChainedElements;
    private List<Element> CalculatedElements;
    private bool chaining = false;
    private bool CanGetHorizontalInput = false;
    private bool CanGetVerticalInput = false;
    protected override bool IfGetInput()
    {
        bool GetInput = false;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space");
            if (!chaining)
            {
                Debug.Log("make close");
                ChainAllClosedElements();
                Debug.Log("maked");
                chaining = true;
            }
            else
            {
                Debug.Log("unmake close");
                UnchainClosedElements();
                chaining = false;
            }
            GetInput = true;
        }
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
    protected override void Move(int Horizontal, int Vertical)
    {
        var canMove = true;
        var direction = new Position(Horizontal, Vertical);
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
            if (element is Character character)
            {
                if (element != this)
                {
                    Debug.Log(element);
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
        Debug.Log("unchain");
        foreach(var element in ChainedElements)
        {
            if(element is Character character && character != this)
            {
                character.EnableInput();
            }
        }
        ChainedElements.Clear();
        CanGetHorizontalInput = false;
        CanGetVerticalInput = false;
    }
}
