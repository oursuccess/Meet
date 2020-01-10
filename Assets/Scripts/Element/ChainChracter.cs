using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainChracter : Character
{
    private List<Element> ChainedElements;
    private SortedSet<Element> CalculatedElements;
    private bool Chaining = false;
    private bool CanGetHorizontalInput = false;
    private bool CanGetVerticalInput = false;
    protected override bool IfGetInput()
    {
        bool GetInput = false;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!Chaining)
            {
                ChainAllClosedElements();
            }
            else
            {
                UnchainClosedElements();
            }
            GetInput = true;
        }
        return GetInput;
    }

    private void ChainAllClosedElements()
    {
        ChainedElements = new List<Element>();
        CalculatedElements = new SortedSet<Element>();
        CalculateClosedOfElement(this);
    }
    private void CalculateClosedOfElement(Element element)
    {
        int xBegin = element.PositionInGrid.x, yBegin = element.PositionInGrid.y;
        Element nextElement = null;
        nextElement = Board.GetElementOfPosition(xBegin + 1, yBegin);
        if (nextElement != null) AddElementToChainedElements(element);
        nextElement = Board.GetElementOfPosition(xBegin - 1, yBegin);
        if (nextElement != null) AddElementToChainedElements(element);
        nextElement = Board.GetElementOfPosition(xBegin, yBegin + 1);
        if (nextElement != null) AddElementToChainedElements(element);
        nextElement = Board.GetElementOfPosition(xBegin, yBegin - 1);
        if (nextElement != null) AddElementToChainedElements(element);
        CalculatedElements.Add(element);
    }
    private void AddElementToChainedElements(Element element)
    {
        if (!ChainedElements.Contains(element))
        {
            ChainedElements.Add(element);
            if(element is Character character)
            {
                character.DisableInput();
            }
        }
    }
    private void ReturnChargeOfMove()
    {

    }
    private void UnchainClosedElements()
    {

    }
}
