using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EndGameScript : MonoBehaviour
{
    public GameObject leaveBtn;
           
    public GameObject finalrematchBtn;
    public GameObject waitingRematchText;
    public GameObject finalleaveBtn;
           
    public GameObject finalText;

    public void EndGame(string text)
    {
        leaveBtn.SetActive(false);
        finalText.SetActive(true);
        finalText.GetComponent<Text>().text = text;
        finalrematchBtn.SetActive(PhotonNetwork.IsMasterClient);
        waitingRematchText.SetActive(!PhotonNetwork.IsMasterClient);
        finalleaveBtn.SetActive(true);
    }

    public void Rematch()
    {
        leaveBtn.SetActive(true);
        finalText.SetActive(false);
        finalrematchBtn.SetActive(false);
        waitingRematchText.SetActive(false);
        finalleaveBtn.SetActive(false);
    }
}
