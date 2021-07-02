using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : NetworkBehaviour
{
    public bool isPressed = false;

    public LayerMask detectable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Check if pressed on server side
    void Update()
    {
        if (isServer)
        {
            CmdCheckPressed();
        }
    }

    //Check if anything is close enough that matches the layermask, if so, set pressed to true, and scale to smalelr
    private void CmdCheckPressed()
    {
        if (isServer)
        {
            if (Physics.CheckSphere(transform.position, 1, detectable))
            {
                RpcSetPressed(true);
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            else
            {
                RpcSetPressed(false);
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
    }

    [ClientRpc]
    void RpcSetPressed(bool _pressed)
    {
        isPressed = _pressed;
    }
}
