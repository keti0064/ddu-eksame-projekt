using UnityEngine.Audio;
using UnityEngine;
using System.Diagnostics;


[System.Serializable]
public class Sound
{
    // data klasse
    
    public string name;

    public AudioClip clip;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;
    public bool SoundEffect;

  

    [HideInInspector]
    public AudioSource source;

     
}