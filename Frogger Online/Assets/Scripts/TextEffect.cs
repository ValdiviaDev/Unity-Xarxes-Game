using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    private Text t;

    public int minFont = 3;
    public int maxFont = 25;

    private float time = 0.0f;

    // Start is called before the first frame update
    void Awake()
    {
        t = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float tim = Mathf.Abs(Mathf.Sin(time));
        t.fontSize = (int)Mathf.Lerp(minFont, maxFont, tim);
        if(t.text.Length > 7)
            t.color = new Color(tim, 1.5f - tim, 1f - tim);
        else
            t.color = new Color(1f - tim, tim, 1.5f - tim);
    }
}
