using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ScoreManager : MonoBehaviour
{

    public GameObject frog1;
    public GameObject frog2;

    public GameObject score1, score2;
    private Text score_text1, score_text2;

    private ScoreController sc1;
    private ScoreController sc2;

    public EndGameScript endGame;

    private float repeat_search_time = 0.0f;
    private float period = 3.0f;

    public int WIN_POINTS = 5;

    // Start is called before the first frame update
    void Start()
    {
        LookForFrog(1);

        LookForFrog(2);

        score_text1 = score1.GetComponent<Text>();
        score_text2 = score2.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > repeat_search_time)
        {
            Debug.Log("Repeating frog score search...");

            repeat_search_time += period;

            if (!frog1 || !sc1)
                LookForFrog(1);

            if (!frog2 || !sc2)
                LookForFrog(2);
        }

        // Update labels if needed
        if (sc1 && sc1.GetIfScoreChanged())
        {
            score_text1.text = sc1.score.ToString();
            sc1.ScoreLabelUpdated();

            if(sc1.score == WIN_POINTS)
            {
                endGame.EndGame(PhotonNetwork.IsMasterClient ? "You\nWIN" : "You\nlose");
            }
        }

        if (sc2 && sc2.GetIfScoreChanged())
        {
            score_text2.text = sc2.score.ToString();
            sc2.ScoreLabelUpdated();

            if (sc2.score == WIN_POINTS)
            {
                endGame.EndGame(!PhotonNetwork.IsMasterClient ? "You\nWIN" : "You\nlose");
            }
        }
    }


    private void LookForFrog(int frog_num)
    {
        switch (frog_num)
        {
            case 1:
                frog1 = GameObject.FindGameObjectWithTag("Frog1");
                if (frog1)
                    sc1 = frog1.GetComponent<ScoreController>();
                break;

            case 2:
                frog2 = GameObject.FindGameObjectWithTag("Frog2");
                if (frog2)
                    sc2 = frog2.GetComponent<ScoreController>();
                break;
        }
    }
}
