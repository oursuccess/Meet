using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBase : Ground
{
    #region Audio
    private AudioSource AudioSource;
    [SerializeField]
    [Tooltip("开门音效")]
    protected AudioClip DoorOpenAudio;
    [SerializeField]
    [Tooltip("无法通过音效")]
    protected AudioClip CantMoveAudio;
    #endregion
    #region Start
    protected override void Start()
    {
        AudioSource = gameObject.AddComponent<AudioSource>();
        base.Start();
    }
    #endregion
    #region DoorOpen
    protected void PlayDoorOpenAudio()
    {
        AudioSource.clip = DoorOpenAudio;
        AudioSource.Play();
    }
    #endregion
    #region CantMove
    protected void PlayCantMoveAudio()
    {
        AudioSource.clip = CantMoveAudio;
        AudioSource.Play();
    }
    #endregion
}
