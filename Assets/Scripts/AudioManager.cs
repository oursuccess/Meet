using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region AudioPath
    public const string AudioPath = "Audios";
    #endregion
    private AudioSource AudioSource;
    void Start()
    {
        PlayBGM();
    }
    private void PlayBGM()
    {
        AudioSource = gameObject.AddComponent<AudioSource>();
        AudioSource.clip = Resources.Load<AudioClip>(AudioPath + "/BGM");
        AudioSource.loop = true;
        AudioSource.Play();
    }
}
