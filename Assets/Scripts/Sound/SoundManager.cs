using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        FireOven,
        KnifeHit,
        DoorHit,
        KidScream,
        Lever,
        CookSteps,
        PaxSteps,
        DogsSteps,
        Shadow3DTransform,
        DoorSqueak,
        KidLaugh,
        PaxJump,
        ToPaxTransfom,
        HoldBox,
        BoxFall,
        Switch,
        CookScream,
        Shadow2D,
        None,
        PaxSandSteps,
        CheckOven,
        Dishes,
        Pan,
        PaxFall,
    };

    public enum SoundType
    {
        Pax = 0,
        NPC,
        Ambient,
    };

    public enum AudioSourcesType
    {
        PaxSteps,
        PaxAction,
        Box,
        Switch,
        Lever,
        Dogs,
        Child,
        Cook,
        Oven,
        TableKnife,
        Dishes,
        PanFrying,
        CheckingOven,
        None,
        PaxFall,
        Door,
    };

    public static AudioSource GetSoundObject(Sound sound, AudioSourcesType type)
    {
        foreach (SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.Instance.soundAudioClips)
        {
            if (soundAudioClip.sound == sound)
                return soundAudioClip.audioSource[0].audioSource[0];
               
        }
        return null;
    }

    public static void PlaySound(Sound sound, SoundType type, AudioSourcesType ASType)
    {
        if (!GetSoundObject(sound, ASType).GetComponent<AudioSource>().isPlaying)
        {
            GetSoundObject(sound, ASType).GetComponent<AudioSource>().PlayOneShot(GetAudioClip(sound));
        }
    }

    public static void StopSound(Sound sound, SoundType type, AudioSourcesType ASType)
    {
        if (GetSoundObject(sound, ASType).GetComponent<AudioSource>().isPlaying)
        {
            GetSoundObject(sound, ASType).GetComponent<AudioSource>().Stop();
        }
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.Instance.soundAudioClips)
        {
            if (soundAudioClip.sound == sound)
                return soundAudioClip.audioClip;
        }
        return null;
    }
}
