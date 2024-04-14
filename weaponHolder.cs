using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class weaponHolder : MonoBehaviour
{

    public Weapon activeWeapon;
    public Weapon startWeapon;

    public float throwForce = 10f;

    [SerializeField]
    Transform meleeHitBoxPos;

    public SpriteRenderer spriteRenderer;
    public Transform weaponSpriteTransform;
    public Collider2D WallCollision;

    [SerializeField]
    GameObject droppedWeaponPrefab;
    [SerializeField]
    GameObject thrownWeaponPrefab;

    [SerializeField] GameObject AmmoImageHolder;
    private List<Image> ammoImagesList = new List<Image>();
    

    [SerializeField] Animator animator;

    Transform canvasTransform;
    public bool canShoot = true;
    
    private void Awake()
    {
        // lav instans af start våbnet og hent canvas transform til ammo visning
        activeWeapon = Instantiate(startWeapon);
        canvasTransform = GameObject.FindGameObjectWithTag("Canvas").transform;
        updateSprite();
    }

    // updater sprite af aktivt våben
    public void updateSprite()
    {
        if (activeWeapon != null){
            spriteRenderer.sprite = activeWeapon.weaponSprite;
            weaponSpriteTransform.localRotation = Quaternion.Euler(0,0,0);
        }else{
            spriteRenderer.sprite = null;
        }

    }
    private void Update(){
        // bruger input
        UserInput();
        

        // tildeler firepoint værdien til våbnet
        if (activeWeapon != null && activeWeapon.firePoint == null){
            activeWeapon.firePoint = gameObject.GetComponent<Transform>();
            activeWeapon.meleeHitBoxPos = meleeHitBoxPos;
        }
        
    }

    // håndtere bruger input
    void UserInput(){
        // hvis der ikke er noget aktivt våben eller spilleren er i kontakt med en væg, så returner
        if(activeWeapon == null || WallCollision.IsTouchingLayers(1 << 7)){
            return;
        }

        // kalder på skyde metoden alt efter om våbnet er auto eller ej
        if (activeWeapon.isAuto && Input.GetButton("Fire1")){
            onShoot();
        }
        else if(Input.GetButtonDown("Fire1")){
            onShoot();
        }
        
        // kaster våben
        if(Input.GetButtonDown("Fire2")){
            ThrowWeapon();
        }

        // reload våben med hensyn til reload tid
        if(Input.GetKeyDown(KeyCode.R)){
            if(activeWeapon != null && !activeWeapon.isMeele && canShoot){
            canShoot = false;
            Invoke("canShootAgain", activeWeapon.reloadTime);
            updateAmmoUI();
            }
        }

    }


    // Skift våben
    public void ChangeWeapon(Weapon newWeapon){
        activeWeapon = newWeapon;
        updateSprite();
        StartCoroutine(AmmoListUpdate());
        canShoot = true;
    }

    // opdater ammo liste efter 0.2 sekunder
    IEnumerator AmmoListUpdate(){
        yield return new WaitForSeconds(0.2f);
        // våben ammo GUI
        for(int i = 0; i < activeWeapon.startAmmo; i++){
        Image newAmmoImage = Instantiate(AmmoImageHolder, canvasTransform).GetComponent<Image>();
        RectTransform newAmmoRectTransform = newAmmoImage.rectTransform;
        newAmmoRectTransform.anchoredPosition = new Vector2(50 + (i * 50), 50);
        newAmmoRectTransform.SetParent(canvasTransform, false);
        newAmmoImage.sprite = activeWeapon.ammoSprite;
        ammoImagesList.Add(newAmmoImage);
        }
        updateAmmoUI();
    }

    // kaster våben
    void ThrowWeapon(){
        if (activeWeapon == null){
            return;
            
        }
        // lav instans af thrownweapon
        GameObject thrownWeaponObject = Instantiate(thrownWeaponPrefab, meleeHitBoxPos.position, meleeHitBoxPos.rotation);
        // find thrownweaponScriptet på thrownweapon objektet og tildel våbenet til det
        thrownWeapon thrownWeaponScript = thrownWeaponObject.GetComponent<thrownWeapon>();
        thrownWeaponScript.weapon = activeWeapon;
        thrownWeaponScript.updateSprite();
        // tilføj kraft til thrownweapon objektet så det flyver
        thrownWeaponObject.GetComponent<Rigidbody2D>().AddForce(transform.up * throwForce, ForceMode2D.Impulse);


        // fjern ammo gui
        for(int i = 0; i < ammoImagesList.Count; i++){
            Destroy((Image)ammoImagesList[i]);
        }
        // tøm ammoImagesList
        ammoImagesList.Clear();

        // sæt aktivt våbnet til null
        activeWeapon = null;

        // kald på update sprite for at fjerne våben sprite fra spillerens hånd
        updateSprite();
        
    }

    // skyd metode
    private float lastShotTime = 0;
    private void onShoot(){
        // returner hvis spiller ikke har et våben
        if (activeWeapon == null){
            return;
        }

        // skyd våbnet hvis nuværende tid er mere end sidste skud tid + våbenets firerate
        if (Time.time - lastShotTime > activeWeapon.firerate && canShoot){
            lastShotTime = Time.time;
            activeWeapon.Shoot("Enemy");
            updateAmmoUI();

            // håndter batt animation
            if(activeWeapon.isMeele){
                switch(Random.Range(0, 2)){
                    case 0:
                        animator.Play("weaponSwing");

                        break;
                    case 1:
                        animator.Play("weaponSwing2");
                        break;
                }
            }

        }
        
    }

    // opdater ammo GUI gennemsigtighed
    private void updateAmmoUI(){

        for(int i = 0; i < ammoImagesList.Count; i++){
            if (i > activeWeapon.Ammo - 1){
                ammoImagesList[i].color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
            }else{
                ammoImagesList[i].color = new Color(1, 1, 1, 1);
            }
        }
    }

    // reload våben og opdater ammo GUI
    private void canShootAgain(){
        activeWeapon.Reload();
        updateAmmoUI();
        canShoot = true;
    }
}
