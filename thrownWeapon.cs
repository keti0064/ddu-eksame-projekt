using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class thrownWeapon : MonoBehaviour
{
    public Weapon weapon; 
    public GameObject droppedWeaponPrefab;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // find spriterenderer og kald dropWeapon efter 1.5 sekund
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Invoke("dropWeapon",1.5f);
    }



    // opdater sprite af våben
    public void updateSprite()
    {
        spriteRenderer.sprite = weapon.sideSprite;
    }

    // drop våben, instantier droppedWeapon og destroy dette objekt
    public void dropWeapon(){
        GameObject droppedWeaponObject = Instantiate(droppedWeaponPrefab, transform.position, transform.rotation);
        DroppedWeapon DroppedWeaponScript = droppedWeaponObject.GetComponent<DroppedWeapon>();
        DroppedWeaponScript.weapon = weapon;
        DroppedWeaponScript.updateSprite();
        Destroy(gameObject);
    }

    // hvis våben rammer fjende, gør skade på fjende og kald dropWeapon
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Enemy"){
            collision.gameObject.GetComponent<EnemyBehavior>().takeDMG(3);
            dropWeapon();
        }

        // hvis våben rammer spiller, kald dropWeapon
        if (collision.gameObject.tag == "Player"){
            dropWeapon();
        }   
    }
}
