using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : NetworkBehaviour
{
    [SyncVar(hook = nameof(ScoreUpdated))]
    public int score = 0;

    void ScoreUpdated(int oldI, int newI)
    {
        GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>().SetText("SCORE\n" + score + "/3");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    float lastTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            
        }
    }
}
