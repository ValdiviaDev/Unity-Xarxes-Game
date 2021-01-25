using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrogMoveAlongPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            if(collision.transform.parent)
                transform.parent = collision.transform.parent.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }

}
