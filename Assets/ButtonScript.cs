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

    void Update()
    {
        if (isServer)
        {
            CmdCheckPressed();
        }
    }

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
