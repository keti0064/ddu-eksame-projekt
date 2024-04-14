using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    private AudioManager am;
    public Slider SoundSlider;
    public Slider MusicSlider;
    
    [SerializeField] GameObject pausePanel;

    private void Start(){
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>(); 
    }

    // start spil igen
    public void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    // load aktive scene
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    // gå til main menu
    public void MainMenu(){
        Time.timeScale = 1;
        OnSceneLeave();
        SceneManager.LoadScene(0);
    }

    // luk spillet
    public void Quit(){
        Application.Quit();
    }


    // håndter input
    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if (pausePanel.activeSelf){
                Resume();
            }else{
                pausePanel.SetActive(true);
                Time.timeScale = 0;
            }
            
        }
    }

    
    private void OnSceneLeave(){
        // opdater audiomanageres gemte sprites
        am.SpriteStatus();

    }


}
