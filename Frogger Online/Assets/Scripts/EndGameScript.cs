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

    [SerializeField] public AudioSource in_game_music;

    [SerializeField] private AudioSource win_game;
    [SerializeField] private AudioSource lose_game;

    public void EndGame(string text)
    {
        leaveBtn.SetActive(false);
        finalText.SetActive(true);
        finalText.GetComponent<Text>().text = text;
        finalrematchBtn.SetActive(PhotonNetwork.IsMasterClient);
        waitingRematchText.SetActive(!PhotonNetwork.IsMasterClient);
        finalleaveBtn.SetActive(true);

        in_game_music.Stop();

        if (text.Length > 7)
        {
            lose_game.Play();
        }
        else
        {
            win_game.Play();
        }
    }

    public void Rematch()
    {
        leaveBtn.SetActive(true);
        finalText.SetActive(false);
        finalrematchBtn.SetActive(false);
        waitingRematchText.SetActive(false);
        finalleaveBtn.SetActive(false);

        lose_game.Stop();
        win_game.Stop();
        in_game_music.Play();
    }
}
