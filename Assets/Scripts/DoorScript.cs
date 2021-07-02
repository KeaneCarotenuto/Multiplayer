using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : NetworkBehaviour
{

    public ButtonScript button;
    public bool pressToClose;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If the associated button is pressed, either open or close the door. Otherwise do the opposite
        if ((button.isPressed && !pressToClose) || (!button.isPressed && pressToClose))
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
