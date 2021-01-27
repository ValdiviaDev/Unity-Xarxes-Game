using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;


public class ScoreController : MonoBehaviourPunCallbacks, IPunObservable
{
    public int score = 0;
    private int current_step = 0; //0: just begun lap, need to touch finish. 1: touched finish (top), need to go back to start

    private bool score_changed = false;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(current_step);
        }
        else
        {
            if (!photonView.IsMine)
                current_step = (int)stream.ReceiveNext();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Start"))
        {
            if (current_step == 1)
            {
                score++;
                score_changed = true;
            }

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

    public bool GetIfScoreChanged()
    {
        return score_changed;
    }

    public void ScoreLabelUpdated()
    {
        score_changed = false;
    }

}
