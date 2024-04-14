using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Gamemanager : MonoBehaviour
{

    [SerializeField]
    GameObject gameWonPanel;

    private void Start(){

    }


    private void Update(){
        // tjekker om der er fjender tilbage, og kald på game won hvis der ikke er flere
        if (countEnemies() == 0){
            Invoke("GameWon",1f);
            // sæt game won panel op
            Instantiate(gameWonPanel, GameObject.FindGameObjectWithTag("Canvas").transform);

        }
    }

    // tæl fjender i scenen
    private int countEnemies(){
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }


    // genstart spillet
    public void GameOver() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // gå til næste scene, hvis spilleren at de har vundet.
    private void GameWon(){
        // håndtere at det er sidste bane
        if (SceneManager.GetActiveScene().buildIndex == 3){
            SceneManager.LoadScene(0);
            
        }else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
