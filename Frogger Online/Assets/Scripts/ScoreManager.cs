using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public GameObject frog1;
    public GameObject frog2;

    private ScoreController sc1;
    private ScoreController sc2;

    private float repeat_search_time = 0.0f;
    private float period = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        LookForFrog(1);

        LookForFrog(2);
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
