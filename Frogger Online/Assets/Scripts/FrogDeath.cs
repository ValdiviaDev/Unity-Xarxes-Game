using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogDeath : MonoBehaviour
{
    private Frog f;
    private Animator animator;
    private bool dead = false;
    private bool floor = false;
    private bool water = false;
    private bool dying = false;

    private int num_of_floors = 0; //Counts num of floor colliders the frog collides with. If <=0 and touching water, then the frog dies


    // Start is called before the first frame update
    void Start()
    {
       f = GetComponent<Frog>();
       animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dying)
        {
            if (dead || (water && !floor && num_of_floors <= 0))
            {
                //Debug.Log("Dead, num floors, "+ num_of_floors);
                animator.SetBool("dead", true);
                dying = true;
                if (f)
                    f.FrogDie();
                else
                    Debug.Log("Can't find Frog component!!!");
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
          
            num_of_floors++;
            Debug.Log("enter floor, " + num_of_floors);
        }


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
            num_of_floors--;
            Debug.Log("leave floor, " + num_of_floors);
        }

    }
}
