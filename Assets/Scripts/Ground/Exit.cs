using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Ground
{
    #region Audio
    private AudioSource AudioSource;
    [SerializeField]
    [Tooltip("脱出音效")]
    private AudioClip ExitAudio;
    #endregion
    protected override void Start()
    {
        Type = GroundType.Exit;
        AudioSource = gameObject.AddComponent<AudioSource>();
        base.Start();
    }
    public override bool ThingCanMoveToMe(Element element)
    {
        if(element.Type == Element.ElementType.Character)
        {
            if(element is Character character)
            {
                PlayExitSound();
                character.ApproachExit();
            }
        }
        return true;
    }
    private void PlayExitSound()
    {
        AudioSource.clip = ExitAudio;
        AudioSource.Play();
    }
}
