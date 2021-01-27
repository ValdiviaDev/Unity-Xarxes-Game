﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerNameSync : MonoBehaviourPunCallbacks, IPunObservable
{
    private Text displayName;

    private void Awake()
    {
        displayName = GetComponent<Text>();

        gameObject.transform.parent = GameObject.Find("Panel").transform;
    }

    #region IPunObservable implementation


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(displayName.text);
        }
        else
        {
            if(!photonView.IsMine)
                this.displayName.text = (string)stream.ReceiveNext();
        }
    }


    #endregion
}
