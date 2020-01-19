using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Element : MonoBehaviour
{
    #region Audio
    [SerializeField]
    [Tooltip("移动音效")]
    protected AudioClip MoveSound;
    protected AudioSource AudioSource;
    #endregion
    #region Type
    public enum ElementType
    {
        Box,
        Bomb,
        Character,
        Key,
    }
    public ElementType Type { get; protected set; }
    #endregion
    #region PositionInGrid
    public PositionInGrid PositionInGrid { get; protected set; }
    public void SetPosition(PositionInGrid position)
    {
        PositionInGrid = position;
    }
    #endregion
    #region Start
    protected virtual void Start()
    {
        AudioSource = gameObject.AddComponent<AudioSource>();
    }
    #endregion
    #region MoveTo
    public bool CanMoveTo(PositionInGrid direction)
    {
        return Board.CanMoveTo(this, direction.x, direction.y);
    }
    public virtual void MoveTo(PositionInGrid direction)
    {
        Board.ElementMoveTo(this, direction.x, direction.y);
        if (AudioSource && MoveSound)
        {
            AudioSource.clip = MoveSound;
            AudioSource.Play();
        }
        OnMoving?.Invoke(this);
        transform.position += new Vector3(direction.x, direction.y);
    }
    public virtual bool ThingCanMoveToMe(Element element, PositionInGrid direction)
    {
        return false;
    }
    public abstract void ThingMoveToMe(Element element, PositionInGrid direction);
    public delegate void MovingDel(Element element);
    public event MovingDel OnMoving;
    #endregion
    #region Board
    protected BoardManager Board;
    public void InitBoard(BoardManager boardManager)
    {
        Board = boardManager;
    }
    #endregion
    #region Chain
    public bool IsChained { get; protected set; } = false;
    public ChainChracter ChainedCharacter { get; protected set; } = null;
    public void ChainTo(ChainChracter chainChracter)
    {
        IsChained = true;
        ChainedCharacter = chainChracter;
    }
    public void UnChain()
    {
        IsChained = false;
        ChainedCharacter = null;
    }
    #endregion
}
