using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIeditor : MonoBehaviour
{
    public GameObject panel;
    public Slider SoundSlider;
    public Slider MusicSlider;

    public Image soundImage;
    public Image musicImage;
    AudioManager am;

    [SerializeField] Sprite soundOff;
    [SerializeField] Sprite soundOn;
    [SerializeField] Sprite MusicOn;
    [SerializeField] Sprite MusicOff;
    void Start()
    {   
        // find AudioManager og tilføj listeners til sliders
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>(); 
        SoundSlider.onValueChanged.AddListener(delegate {ValueSoundEffectChange(); });
        MusicSlider.onValueChanged.AddListener(delegate {ValueMusicChange(); });
        if(GameObject.Find("CanvasMenu") == true){
            am.Play("BackgroundMusic");
        }
        else{
            am.StopSound("BackgroundMusic");
            

        }
        // opdater mute sprite hvis der er data gemt på audiomanageren
        if (am.soundButton != null){
            musicImage.sprite = am.musicButton;
            soundImage.sprite = am.soundButton;
        }
        
        

        
    }
    void Update()
    {   
        try
        {
            if(GameObject.Find("CanvasMenu") == true && am.SoundEffectVolume == 0.1 && am.MusicVolume == 0.1){
            am.SoundEffectVolume = SoundSlider.value;
            am.MusicVolume = MusicSlider.value; 
            

            }
            else{
            
            SoundSlider.value = am.SoundEffectVolume;
            MusicSlider.value = am.MusicVolume;    
            }
        }
        catch
        {
            Debug.Log("the MainMenu is not loaded yet or the AudioManager is not found this is not an error");
        }        
    }


    // skift sprite på soundbutton og mute lydeffekter
    public void SoundButton(){
        if (soundImage.sprite == soundOn)
        {
            soundImage.sprite = soundOff;
            
        }
        else
        {
            soundImage.sprite = soundOn;
        }
        am.MuteSoundEffect();
        
    }
    // skift sprite på musicbutton og mute musik
    public void MusicButton(){
        if (musicImage.sprite == MusicOn)
        {
            musicImage.sprite = MusicOff;
        }
        else
        {
            musicImage.sprite = MusicOn;
        }
        am.MuteSoundMusic();
    }
    

    // hent lydeffekt og musik slider værdier, gem i audiomanager
    public void ValueMusicChange()
    {
        am.MusicVolume = MusicSlider.value;
    }
    public void ValueSoundEffectChange()
    {
        am.SoundEffectVolume = SoundSlider.value;
    }

    // skift scene til første bane
    public void ScreenChange()
    {
        am.SpriteStatus();
        SceneManager.LoadScene(1);

    }

    // luk spillet
    public void QuitGame()
    {
        Application.Quit();
    }
}
 