using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreController : MonoBehaviour
{
    public int score = 0;
    private int current_step = 0; //0: just begun lap, need to touch finish. 1: touched finish (top), need to go back to start
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Start"))
        {
            if (current_step == 1)
                score++;

            current_step = 0;
            
        }


        if (collision.gameObject.layer == LayerMask.NameToLayer("Finish"))
        {
            current_step = 1;
        }
    }

    public void ResetStep()
    {
        current_step = 0;
    }

}
