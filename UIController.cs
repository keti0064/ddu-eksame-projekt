using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UIController : MonoBehaviour
{
    
    public TMP_Text healthText;
    public Image weaponImage;
    
    private void Update(){
        // ammo og våben billede
        weaponHolder wp = GameObject.Find("WeaponHolder").GetComponent<weaponHolder>();
        Weapon currentWeapon = wp.activeWeapon;
        if (currentWeapon != null){
            weaponImage.gameObject.SetActive(true);
            weaponImage.sprite = currentWeapon.sideSprite;     
            if (wp.canShoot){
                weaponImage.color = new Color(1,1,1,1);
            }
            else{
                weaponImage.color = new Color(0.5f,0.5f,0.5f,0.8f);
            } 

        }
        else{
            weaponImage.sprite = null;
            weaponImage.gameObject.SetActive(false);
            
        }

         

    }
}
