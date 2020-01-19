using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SwitchBase : Ground
{
    #region Sprite
    [SerializeField]
    [Tooltip("开启时的贴图")]
    protected Sprite OpenSprite;
    [SerializeField]
    [Tooltip("关闭时的贴图")]
    protected Sprite CloseSprite;
    #endregion
    #region Audio
    private AudioSource AudioSource;
    [SerializeField]
    private AudioClip Switched;
    #endregion
    protected override void Start()
    {
        AudioSource = gameObject.AddComponent<AudioSource>();
        base.Start();
    }
    public bool Touched { get; protected set; } = false;
    protected void ChangeTouchState()
    {
        ChangeTouchState(!Touched);
    }
    protected void ChangeTouchState(bool state)
    {
        Touched = state;
        gameObject.GetComponent<SpriteRenderer>().sprite = Touched ? OpenSprite : CloseSprite;
        AudioPlay();
    }
    private void AudioPlay()
    {
        AudioSource.clip = Switched;
        AudioSource.Play();
    }
}
