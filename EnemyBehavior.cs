using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    [SerializeField] enemyWeaponHolder weaponHolder;
    [SerializeField] GameObject bodyObject;
[SerializeField] GameObject damageEffect;
    public int health;
   
   // tjek om fjenden har liv tilbage
    void Update()
    {
        if(health <= 0){
        destroySelf();
        }
    }

    // fjern fjenden og instantier et lig
    void destroySelf(){
    weaponHolder.ChangeWeapon(null);
    Instantiate(bodyObject, transform.position, transform.rotation);
    ps.Play();
    Destroy(gameObject);
}

    // tag skade
    public void takeDMG(int dmg){
    ps.Play();
    Instantiate(damageEffect, transform.position, transform.rotation);
    health -= dmg;
}



}
