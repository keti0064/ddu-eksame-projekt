using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    [SerializeField] weaponHolder wh;
    [SerializeField] Gamemanager gm;

    [SerializeField] GameObject bloodSplat;
    public int health;


    // tag skade, kald på blod effekt
    public void takeDMG(int dmg){  
        ps.Play();
        health -= dmg;
        dropBlood();
    }


    // tjek om spilleren har liv tilbage
    private void FixedUpdate(){
        if (health <= 0){
            gm.GameOver();
        }

        if(health < 5){
            dropBlood();

        }

    }


    // cheat kode til at give spilleren 1 milliard liv

    [SerializeField] GameObject cheatPanel;
    private void Update(){
        if (Input.GetKeyDown(KeyCode.P)){
            health = 1000000000;
            Instantiate(cheatPanel, GameObject.FindGameObjectWithTag("Canvas").transform);
        }
    }


    // instantier blod effekt
    private void dropBlood(){
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Instantiate(bloodSplat, transform.position, transform.rotation);
        }
    }
}
