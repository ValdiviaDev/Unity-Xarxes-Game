using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogDeath : MonoBehaviour
{

    private Animator animator;
    private bool dead = false;
    private bool floor = false;
    private bool water = false;
    private bool dying = false;

    public LayerMask floor_layer;
    public LayerMask water_layer;
    public LayerMask enemy_layer;


    public

    // Start is called before the first frame update
    void Start()
    {
       animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dying)
        {
            if (dead || (water && !floor))
            {
                Debug.Log("Ore wa deado");
                animator.SetBool("dead", true);
                dying = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.layer == LayerMask.NameToLayer("EnemyCol"))
        {
            dead = true;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("WaterCol"))
        {
            water = true;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("FloorCol"))
        {
            floor = true;
        }

        //if (collision.IsTouchingLayers(enemy_layer))
        //{
        //    dead = true;
        //}
        //else
        //{
        //    if (collision.IsTouchingLayers(water_layer))
        //    {
        //        dead = true;
        //    }

        //    if (collision.IsTouchingLayers(floor_layer))
        //    {
        //        dead = false;
        //    }


        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyCol"))
        {
            dead = false;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("WaterCol"))
        {
            water = false;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("FloorCol"))
        {
            floor = false;
        }

    }
}
