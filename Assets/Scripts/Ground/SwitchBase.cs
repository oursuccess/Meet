using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SwitchBase : Ground
{
    [SerializeField]
    [Tooltip("开启时的贴图")]
    protected Sprite OpenSprite;
    [SerializeField]
    [Tooltip("关闭时的贴图")]
    protected Sprite CloseSprite;
    public bool Touched { get; protected set; } = false;
    protected void ChangeTouchState()
    {
        ChangeTouchState(!Touched);
    }
    protected void ChangeTouchState(bool state)
    {
        Touched = state;
        gameObject.GetComponent<SpriteRenderer>().sprite = Touched ? OpenSprite : CloseSprite;
    }
}
