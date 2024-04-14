using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DroppedWeapon : MonoBehaviour
{

    public Weapon weapon;
    public Weapon StartWeapon;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        // tjek om der er et start våben og instantier det
        if (StartWeapon != null)
        {
            weapon = Object.Instantiate(StartWeapon);
        }
        
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        updateSprite();
    }

    // update sprite
    public void updateSprite()
    {
        spriteRenderer.sprite = weapon.sideSprite;
    }


    // hvis spilleren er i triggeren og trykker m1, så skift våben
    private bool isInside = false;
    weaponHolder wpHolder;

    // spiller går ind i trigger
    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag =="Player"){
            isInside = true;
            wpHolder = collision.gameObject.GetComponentInChildren<weaponHolder>();
        }

    }

    // spiller går ud af trigger
    void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.tag =="Player"){
            isInside = false;
            wpHolder = null;
        }

    }


    // bruger late update for ikke at kunne samle op og kaste samtidig
    private void LateUpdate(){
    if (Input.GetButtonDown("Fire2") && isInside){
        wpHolder.ChangeWeapon(weapon);
        Destroy(gameObject);
    }
    }

    

    
}