using UnityEngine.Audio;
using UnityEngine;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    [HideInInspector]
    public Sprite  soundButton;

    [HideInInspector]
    public Sprite  musicButton;

    public float SoundEffectVolume;
    public float MusicVolume;

    
    public static AudioManager instance;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.loop = sound.loop;
        }
    }
    
    void Update(){
        // opdater lyd volumen på alle clips
        foreach (Sound sound in sounds)
        {
        if(sound.SoundEffect){
            sound.source.volume = SoundEffectVolume;
        }else{
            sound.source.volume = MusicVolume;
        }
        }

    }
    


    // spil lyd ud fra navn som input
    public void Play (string name)
    {
        int index = 0;

        // gennemgå alle gemte lyde og led efter den med det rigtige navn
        foreach (Sound sound in sounds)
        {
            index++;
            if (sound.name == name)
            {
                sound.source.pitch = sound.pitch;
                if (sound.SoundEffect){
                    sound.source.PlayOneShot(sound.source.clip);
                                        

                }
                else if (!sound.SoundEffect){
                    Debug.Log("MusicVolume: " + MusicVolume);
                    sound.source.Play();

                }

                
            }

        }
    }

    // stop lyd ud fra navn som input
    public void StopSound(string name)
    {
        int index = 0;
        foreach (Sound sound in sounds)
        {
            index++;
            if (sound.name == name)
            {
                sound.source.Stop();
            }
        }
    }
    // slukker og tænder for alle lyde
    public void MuteSoundMusic()
    {
        foreach (Sound sound in sounds)
        {
            if (!sound.SoundEffect)
            {
                sound.source.mute = !sound.source.mute;
            }
            else{
            }
        }
    }
    // slukker og tænder for alle lyde
    public void MuteSoundEffect()
    {
        foreach (Sound sound in sounds)
        {
            if (sound.SoundEffect )
            {
                
                sound.source.mute = !sound.source.mute;
            }
        }
    }

    // gem sprite status af sound og music button
    public void SpriteStatus(){
        soundButton = GameObject.Find("SoundButton").GetComponent<UnityEngine.UI.Image>().sprite;
        musicButton = GameObject.Find("MusicButton").GetComponent<UnityEngine.UI.Image>().sprite;
    }
}