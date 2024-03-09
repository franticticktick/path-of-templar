using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class NamedAudioClip
{
    public string name;
    public float volume;
    public List<AudioClip> audioClips;

    public AudioClip RandomAudioClip()
    {
        if(audioClips.Count != 1)
        {
            System.Random random = new();
            int index = random.Next(audioClips.Count);
            return audioClips[index];
        }
        return audioClips.First();
    }
}
