using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour
{
    public GameObject leaveBtn;

    public GameObject finalrematchBtn;
    public GameObject finalleaveBtn;

    public GameObject finalText;

    public void EndGame(string text)
    {
        leaveBtn.SetActive(false);
        finalText.SetActive(true);
        finalText.GetComponent<Text>().text = text;
        finalrematchBtn.SetActive(true);
        finalleaveBtn.SetActive(true);
    }
}
