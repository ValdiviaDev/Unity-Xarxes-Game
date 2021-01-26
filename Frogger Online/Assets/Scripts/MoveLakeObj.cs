using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class MoveLakeObj : MonoBehaviour
{
    public float speed = 1.0f;


    private float timer_to_delete = 0.0f;
    private float time_to_delete = 20.0f;

    public enum dir
    {
        Left,
        Right
    };

    public dir direction = dir.Right;
    

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //Move forward
            Vector3 forward;

        if (direction == dir.Right)
            forward = new Vector3(transform.right.x, transform.right.y);
        else
            forward = new Vector3(-transform.right.x, -transform.right.y);

        
            transform.position = transform.position + forward * Time.deltaTime * speed;
        }
    }
}
