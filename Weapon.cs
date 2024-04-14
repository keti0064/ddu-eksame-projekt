using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{


    // våben data:
    public bool isAuto;
    public bool isMeele;
    public float meleeRadius;
    public int Ammo;
    public int startAmmo;
    public int damage = 1;
    public int bulletsPerShot;
    public float bulletSpread;

    public float firerate;
    public float reloadTime;

    public float shakeDureation;
    public float shakeAmount;
    public string soundName;
    // prefab ref:
    public Bullet bullet;

    //Sprite ref:
    public Sprite weaponSprite;
    public Sprite sideSprite;

    public Sprite ammoSprite;

    // værdi tildeles i script
    public Transform meleeHitBoxPos;
    public Transform firePoint;

    
    private void Awake(){
         Ammo = startAmmo;
    }
    
    // angrib metode
    public void Shoot(string targetTag){

        // skyde våben:
        if (!isMeele && Ammo > 0){
            // cam shake
            Camera.main.GetComponent<cameraShake>().ShakeCamera(shakeDureation, shakeAmount);

            FindObjectOfType<AudioManager>().Play(soundName);
            // instantiate alle skud.
            for (int i = 0; i < bulletsPerShot; i++){
                Quaternion rot = Quaternion.Euler(0, 0, firePoint.rotation.z + Random.Range(-bulletSpread, bulletSpread));
                Bullet _bullet = Instantiate(bullet, firePoint.position, firePoint.rotation * rot);
                _bullet.targetTag = targetTag;
                _bullet.damage = damage;
                
            }
            Ammo -= 1;
            
            
        }

        // Meele våben:
        if (isMeele){
            Collider2D[] results = Physics2D.OverlapCircleAll(meleeHitBoxPos.position, meleeRadius, LayerMask.GetMask("Enemy"));
            

            FindObjectOfType<AudioManager>().Play(soundName);
            if (results.Length > 0){
                // kald på cam shake og lyd
                Camera.main.GetComponent<cameraShake>().ShakeCamera(shakeDureation, shakeAmount);
                FindObjectOfType<AudioManager>().Play("bathit");

            }
            // gør skade på alle fjender i radius
            foreach (Collider2D hit in results){
                hit.gameObject.GetComponent<EnemyBehavior>().takeDMG(damage); 
            }
        }
    }

    // reload våbnet
    public void Reload(){
        Ammo = startAmmo;
    }

}
