using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    #region Type
    public enum GroundType 
    {
        Exit,
        Floor,
        Wall,
    }
    public GroundType Type { get; protected set; }
    #endregion
    public virtual bool ThingCanMoveToMe(Element element)
    {
        return false;
    }
}
