using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWeaponHolder : MonoBehaviour
{
    public Weapon activeWeapon;
    public Weapon startWeapon;

    public float throwForce = 10f;

    [SerializeField]
    Transform meleeHitBoxPos;

    public SpriteRenderer spriteRenderer;

    [SerializeField]
    GameObject droppedWeaponPrefab;
    [SerializeField]
    GameObject thrownWeaponPrefab;

    private Transform playerTransform;

    private void Awake()
    {
        activeWeapon = Object.Instantiate(startWeapon);
        playerTransform = GameObject.Find("Blayer").GetComponent<Transform>();
        if (activeWeapon != null && activeWeapon.firePoint == null){
            activeWeapon.firePoint = gameObject.GetComponent<Transform>();
            activeWeapon.meleeHitBoxPos = meleeHitBoxPos;
        }
        updateSprite();
    }

    // opdater sprite af aktivt våben
    public void updateSprite()
    {
        if (activeWeapon != null){
            spriteRenderer.sprite = activeWeapon.weaponSprite;
            transform.localRotation = Quaternion.Euler(0,0,0);
        }else{
            spriteRenderer.sprite = null;
        }

    }

    // håndtere våben skift
    public void ChangeWeapon(Weapon newWeapon){
        Weapon oldWeapon = activeWeapon;
        activeWeapon = newWeapon;
        updateSprite();
        if (oldWeapon != null){
            GameObject droppedWeaponObject = Instantiate(droppedWeaponPrefab, transform.position, transform.rotation);
            DroppedWeapon DroppedWeaponScript = droppedWeaponObject.GetComponent<DroppedWeapon>();
            DroppedWeaponScript.weapon = oldWeapon;
            DroppedWeaponScript.updateSprite();
        }
        
        
        
    }


    // skyd aktivt våben
    private float lastShotTime = 0;
    public void onShoot(){
        if (activeWeapon == null){
            return;
        }
        

        // skyd våbnet hvis nuværende tid er mere end sidste skud tid + våbenets firerate
        if (Time.time - lastShotTime> activeWeapon.firerate*Random.Range(1.8f, 2.2f)){
            lastShotTime = Time.time;
            activeWeapon.Shoot("Player");
        }

        // reload aktive våben hvis ammo er 0
        if(activeWeapon.Ammo == 0){
            activeWeapon.Reload();
            lastShotTime = Time.time + activeWeapon.reloadTime*2f;
        }
        
    }
}
