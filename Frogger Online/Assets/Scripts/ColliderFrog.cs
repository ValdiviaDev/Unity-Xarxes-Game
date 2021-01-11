using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderFrog : MonoBehaviour
{
    private Frog frog;
    public Frog.dir direction;

    private CircleCollider2D coll;
    public LayerMask layer;

    void Start()
    {
        coll = GetComponent<CircleCollider2D>();
        frog = GetComponentInParent<Frog>(); //Test
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Enters colision");
        if(collision.gameObject.layer == layer)
        {
            Debug.Log("Enters frog layer");
            frog.cant_move_to = direction;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Exits colision");
        if (collision.gameObject.layer == layer)
        {
            Debug.Log("Exits frog layer");
            frog.cant_move_to = Frog.dir.none;
        }
    }

}
