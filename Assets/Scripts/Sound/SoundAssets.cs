using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAssets : MonoBehaviour
{
    public static SoundAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public SoundAudioClip[] soundAudioClips;
    public AudioSourceReference[] audioSourcesReferences;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
        public SoundManager.SoundType soundtype;
        public AudioSourceReference[] audioSource;
    }

    [System.Serializable]
    public class AudioSourceReference
    {
        public SoundManager.AudioSourcesType asType;
        public AudioSource[] audioSource;
    }
}

