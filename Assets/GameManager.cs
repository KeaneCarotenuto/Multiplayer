using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += LoadedLevel;
    }

    void UpdateScore(int oldP, int newP)
    {
        Debug.Log("UPDATED");
        //points = newP;
    }

    [Server]
    void LoadedLevel(Scene scene, LoadSceneMode mode)
    {
        if (isServer)
        {
            GameObject.Find("ScoreCounter").GetComponent<ScoreCounter>().score = 0;
        }
    }

    void Update()
    {
        
    }
}
