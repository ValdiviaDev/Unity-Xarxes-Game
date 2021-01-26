using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrogMoveAlongPlatform : MonoBehaviour
{
    private Frog f;

    private void Awake()
    {
        f = GetComponent<Frog>();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        // This is to move the frog allongside the platform
        if(collision.gameObject.tag == "Platform")
        {
            MoveLakeObj moveLakeObj = collision.gameObject.GetComponent<MoveLakeObj>();
            if(moveLakeObj.direction == MoveLakeObj.dir.Right)
                f.cum_speed = moveLakeObj.speed;
            else
                f.cum_speed = -moveLakeObj.speed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            f.cum_speed = 0.0f;
        }
    }

}
