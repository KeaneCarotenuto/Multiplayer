using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : NetworkBehaviour
{

    public Transform spearPosition;

    public GameObject currentSpear;

    public List<GameObject> newSpears;
    public List<GameObject> oldSpears;

    public float maxSpears;

    public GameObject spearPrefab;

    public Camera playerCamera;

    public LayerMask[] targetable;

    // Start is called before the first frame update
    void Start()
    {
        if (currentSpear)
        {
            currentSpear.GetComponent<Spear>().isHeld = true;
            currentSpear.GetComponent<Spear>().isThrown = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            for (int i = oldSpears.Count - 1; i >= 0; i--)
            {
                if (!oldSpears[i]) oldSpears.RemoveAt(i);
            }

            if (currentSpear == null && oldSpears.Count < maxSpears)
            {
                //LocalLoadNewSpear();
            }

            if (Input.GetMouseButtonDown(0) && oldSpears.Count < maxSpears)
            {
                //if (isClient)
                //{
                //    ServerLoadNewSpear();
                //}

                //LocalLoadNewSpear();
                //currentSpear.GetComponent<Spear>().Throw();
                //oldSpears.Add(currentSpear);
                //currentSpear = null;

                //noPar();

                //LocalLoadNewSpear();
                //currentSpear.GetComponent<Spear>().Thorw();
                //oldSpears.Add(currentSpear);
                //currentSpear = null;

                CmdServerLoadNewSpear();
            }

            if (Input.GetMouseButtonDown(1) && oldSpears.Count > 0)
            {
                oldSpears[0].GetComponent<Spear>().returnToHand = true;
                oldSpears[0].transform.parent = spearPosition;

                newSpears.Add(oldSpears[0]);
                oldSpears.RemoveAt(0);
            }
        }
    }

    [ClientRpc]
    void noPar()
    {
        currentSpear.transform.parent = null;
    }

    [Command]
    private void CmdServerLoadNewSpear()
    {
        //currentSpear = Instantiate(spearPrefab, spearPosition, false);
        GameObject s = Instantiate(spearPrefab, spearPosition, false);
        NetworkServer.Spawn(s);

        s.GetComponent<Spear>().playerCam = playerCamera;
        s.GetComponent<Spear>().player = gameObject;
        s.GetComponent<Spear>().playerSpearPos = spearPosition;
        s.GetComponent<Spear>().isHeld = true;
        s.GetComponent<Spear>().isThrown = false;
        s.GetComponent<Spear>().Throw();

        NetworkServer.Spawn(s);
        //NetworkServer.SpawnObjects();
    }

    private void LocalLoadNewSpear()
    {
        currentSpear = newSpears[0];
        newSpears.RemoveAt(0);

        currentSpear.SetActive(true);

        currentSpear.transform.localPosition = Vector3.zero;

        //currentSpear = Instantiate(spearPrefab, spearPosition, false);
        currentSpear.GetComponent<Spear>().playerCam = playerCamera;
        currentSpear.GetComponent<Spear>().player = gameObject;
        currentSpear.GetComponent<Spear>().playerSpearPos = spearPosition;
        currentSpear.GetComponent<Spear>().isHeld = true;
        currentSpear.GetComponent<Spear>().isThrown = false;
        currentSpear.GetComponent<Spear>().returnToHand = false;
    }
}
