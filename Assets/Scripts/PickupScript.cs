using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : NetworkBehaviour
{
    [Server]
    private void OnTriggerEnter(Collider other)
    {
        //If player touches green thing, add to score
        if (isServer && NetworkServer.active)
        {
            Debug.Log("PICKUP");
            if (other.transform.root.name.Contains("Player"))
            {
                NetworkServer.UnSpawn(gameObject);
                NetworkServer.Destroy(gameObject);
                GameObject.Find("ScoreCounter").GetComponent<ScoreCounter>().score++;
            }
        }
    }
}
