using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : NetworkBehaviour
{

    public Transform spearPosition;

    public GameObject spearPrefab;

    public Camera playerCamera;

    public LayerMask noThrow;

    public GameObject noThrowScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        Time.timeScale = 1.0f;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                //  SceneManager.LoadScene(0);

                ReloadScene();

                //NetworkServer.SpawnObjects();
            }

            if (Physics.CheckSphere(transform.position, 0.5f, noThrow))
            {
                noThrowScreen.SetActive(true);
            }
            else
            {
                noThrowScreen.SetActive(false);
                if (Input.GetMouseButtonDown(0))
                {
                    CmdServerLoadNewSpear();
                }
            }
        }
        else
        {
            noThrowScreen.SetActive(false);
        }
    }

    [Command]
    private void ReloadScene()
    {
        NetworkManager.singleton.ServerChangeScene("SampleScene");
    }

    [Command]
    private void CmdServerLoadNewSpear()
    {
        GameObject s = Instantiate(spearPrefab, spearPosition, false);
        NetworkServer.Spawn(s);

        s.GetComponent<Spear>().playerCam = playerCamera;
        s.GetComponent<Spear>().player = gameObject;
        s.GetComponent<Spear>().playerSpearPos = spearPosition;
        s.GetComponent<Spear>().isHeld = true;
        s.GetComponent<Spear>().isThrown = false;
        s.GetComponent<Spear>().Throw();
        s.GetComponent<Spear>().spawnTime = Time.time;
    }
}
