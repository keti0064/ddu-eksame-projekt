using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public class seeBlayer : MonoBehaviour
{
   
    Transform player;

    // A* variabler

    public Transform[] patrolePosition;

    private Transform[] CurrentPosition;

    public int delay = 0;
    int index = 0;
    
    IAstarAI agent;

    float switchTime = float.PositiveInfinity;
    
    // aggro variabler
    float firstAggroTime = -1f;
    public float aggroTime = 0.5f;

    AIDestinationSetter AD;

    // shooting at player:
    bool playerAggro = false;
    [SerializeField]
    enemyWeaponHolder weaponHolder;


    void Start()
    {
        // find spiller objekt og relevante komponenter
        player = GameObject.Find("Blayer").transform;
        AD = GetComponent<AIDestinationSetter>();
        agent = GetComponent<IAstarAI>();
        CurrentPosition = patrolePosition;
        

    }
    
    void Update(){
    // tjek om spiller er indenfor synsvidde
    if((player.transform.position - gameObject.transform.position).magnitude < 20f)
    {
        // lav raycast fra fjende til spilleren
        Vector2 mouseDirection = player.position - gameObject.transform.position;
        Ray2D ray = new Ray2D(gameObject.transform.position, mouseDirection.normalized);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 20f, (1 << 7) | (1 << 9));
        // tjek om spilleren er første objekt i raycast
        if(hit.collider != null && hit.collider.CompareTag("Player"))
        {
            // spilleren er set

            // håndter første aggro tid
            if (playerAggro == false){
                firstAggroTime = Time.time;
            }
            playerAggro = true;
            AD.target = player;
            CurrentPosition = null;
        }
        else
        {
            // spiller er ikke set
            playerAggro = false;
            AD.target = null;
            CurrentPosition = patrolePosition;
            Patrole();
        }
    }
    else
    // spiller er ude af synsvidde
    {
        Patrole();
    }

    // hvis spilleren er set og første aggro tid er gået, skyd
    if(playerAggro && firstAggroTime + aggroTime < Time.time){
        weaponHolder.onShoot();
    }
    
    
    }


    // patruljer mellem positioner
    void Patrole(){  
        
        // håndter om der er gemte patrulje punkter
        if(CurrentPosition == null || CurrentPosition.Length == 0) return;


        // skift til næste patrulje punkt
        bool search = false;
        if (agent.reachedEndOfPath && !agent.pathPending && float.IsPositiveInfinity(switchTime)){
            switchTime = Time.time + delay;
        }
        if (Time.time >= switchTime){
            index ++;
            search = true;
            switchTime = float.PositiveInfinity;
        }
        
        index %= CurrentPosition.Length;
        agent.destination = CurrentPosition[index].position;
        if (search) agent.SearchPath();
    }



}