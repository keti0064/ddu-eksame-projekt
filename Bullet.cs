using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public string targetTag;

    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        // tilfældig hastighed for variation
        rb2d.velocity = transform.up * speed*Random.Range(0.9f,1.1f);
        // destroy sig selv efter 3 sekunder
        Invoke("destroySelf",3);
        
    }

    void destroySelf(){
        Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D col)
    {   

        // Gør skade på fjende
        if (targetTag == "Enemy" && col.gameObject.tag == "Enemy"){
            col.gameObject.GetComponent<EnemyBehavior>().takeDMG(damage);
            Destroy(gameObject);
        
        }
        // gør skade på spiller
        if (targetTag == "Player" && col.gameObject.tag == "Player"){
            col.gameObject.GetComponent<PlayerHealth>().takeDMG(damage);
            Destroy(gameObject);
        }

        // hvis kuglen rammer væg, destroy
        if(col.gameObject.layer == 7){
            destroySelf();
        }
        
    }

}
