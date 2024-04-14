using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splatterChooser : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public Sprite[] splatterSprites;

    void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        // find det aktive våben
        Weapon player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<weaponHolder>().activeWeapon;
        
        // vælg splatter sprite baseret på våben brugt
        switch(player.weaponSprite.name){
            case "gatten":
                spriteRenderer.sprite = splatterSprites[0];
                break;
            case "shotguntop1":
                spriteRenderer.sprite = splatterSprites[3];
                break;
            
            case "batt":
                spriteRenderer.sprite = splatterSprites[2];
                break;
            default:
                spriteRenderer.sprite = splatterSprites[3];
                break;
        }

        

    }
}
