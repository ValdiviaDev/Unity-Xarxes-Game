using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerNameSync : MonoBehaviourPunCallbacks, IPunObservable
{
    public bool isP1 = false;
    private Text displayName;

    private bool imEditable = true;

    private void Awake()
    {
        displayName = GetComponent<Text>();
        if (isP1 && PhotonNetwork.IsMasterClient) imEditable = false;
        else if(!isP1 && !PhotonNetwork.IsMasterClient) imEditable = false;
    }

    public void LinkMyName()
    {
        displayName.text = PhotonNetwork.NickName;
    }

    #region IPunObservable implementation


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting && !imEditable)
        {
            stream.SendNext(displayName.text);
        }
        else
        {
            if(imEditable)
                this.displayName.text = (string)stream.ReceiveNext();
        }
    }


    #endregion
}
