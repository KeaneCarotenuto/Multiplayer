using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : NetworkBehaviour
{
    [SyncVar(hook = nameof(ScoreUpdated))]
    public int score = 0;

    //Updates text and client scores when score changes
    public void ScoreUpdated(int oldI, int newI)
    {
        GameObject st = GameObject.Find("ScoreText");

        if (st)
        {
            TextMeshProUGUI text = st.GetComponent<TextMeshProUGUI>();

            text.SetText("GREEN\n" + score + "/4");

            if (score >= 4)
            {
                text.SetText("GREEN\n" + score + "/4\nPress [R] to restart!");
                text.color = Color.green;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        ScoreUpdated(0, 0);
    }

    float lastTime = 0;

    // Update is called once per frame
    void Update()
    {

    }
}
