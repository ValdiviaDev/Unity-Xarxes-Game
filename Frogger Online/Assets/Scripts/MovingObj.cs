﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class MovingObj : MonoBehaviour
{
    private Rigidbody2D rb;

    public float minSpeed = 8.0f;
    public float maxSpeed = 12.0f;

    private float speed = 1.0f;

    public enum dir
    {
        Left,
        Right
    };

    public dir direction = dir.Right;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void FixedUpdate()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //Move forward
            Vector2 forward;

            if (direction == dir.Right)
                forward = new Vector2(transform.right.x, transform.right.y);
            else
                forward = new Vector2(-transform.right.x, -transform.right.y);

            rb.MovePosition(rb.position + forward * Time.fixedDeltaTime * speed);


        }
    }

}
