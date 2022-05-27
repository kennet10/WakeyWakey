using UnityEngine.Audio;
using UnityEngine;

// A class that stores the name of a sound, the audio clip, and its volume, pitch, etc.
[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
