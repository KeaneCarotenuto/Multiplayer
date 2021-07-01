using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : NetworkBehaviour
{

    public ButtonScript button;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (button.isPressed)
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
