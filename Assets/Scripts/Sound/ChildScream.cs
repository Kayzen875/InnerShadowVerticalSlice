using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildScream : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Screaming", 60);
        Invoke("Laugh", 60);
    }

    public void Screaming()
    {
        SoundManager.PlaySound(SoundManager.Sound.KidScream, SoundManager.SoundType.NPC, SoundManager.AudioSourcesType.Child);
        Invoke("Screaming", 35);
    }

    public void Laugh()
    {
        SoundManager.PlaySound(SoundManager.Sound.KidLaugh, SoundManager.SoundType.NPC, SoundManager.AudioSourcesType.Child);
        Invoke("Laugh", 15);
    }
}
