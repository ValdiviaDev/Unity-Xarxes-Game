using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestroyMovObj : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(collision.gameObject);
        }
    }
}
