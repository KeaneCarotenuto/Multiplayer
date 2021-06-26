using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : NetworkBehaviour
{
    public float throwSpeed;

    public float gravity;

    public bool isThrown;
    public bool isHeld;
    public bool returnToHand;

    public Transform tip;
    public Transform body;

    public GameObject player;

    public Camera playerCam;

    public Transform playerSpearPos;

    public LayerMask hitMask;

    public Vector3 velocity;

    public GameObject spearPrefab;

    Vector3 landPos;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isServer)
        {
            if (isThrown)
            {
                velocity.y += gravity * Time.deltaTime;

                transform.LookAt(transform.position + velocity);

                transform.position += velocity * Time.deltaTime;

                RaycastHit[] HitInfo = Physics.BoxCastAll(tip.position, tip.localScale, transform.forward, tip.rotation, 0.01f, hitMask);

                foreach (RaycastHit _hit in HitInfo)
                {
                    if (_hit.transform.parent && _hit.transform.parent.GetComponent<Spear>())
                    {
                        if (!_hit.transform.parent.GetComponent<Spear>().isThrown && !_hit.transform.parent.GetComponent<Spear>().isHeld)
                        {
                            Stick(_hit.transform.parent);
                            //transform.parent = _hit.transform.parent;
                            break;
                        }
                    }
                    else
                    {
                        Stick(_hit.transform);
                        //transform.parent = _hit.transform;
                        break;
                    }
                }


            }
            else
            {
                if (isHeld)
                {

                    RaycastHit[] HitInfo = Physics.RaycastAll(playerCam.transform.position, playerCam.transform.forward, 100f, hitMask);

                    foreach (RaycastHit _hit in HitInfo)
                    {
                        if (_hit.transform.parent && _hit.transform.parent.GetComponent<Spear>())
                        {
                            if (!_hit.transform.parent.GetComponent<Spear>().isThrown && !_hit.transform.parent.GetComponent<Spear>().isHeld)
                            {
                                transform.LookAt(_hit.point, Vector3.up);
                                break;
                            }
                        }
                        else
                        {
                            transform.LookAt(_hit.point, Vector3.up);
                            break;
                        }
                    }
                }
                else
                {
                    //transform.position = landPos;
                }

                if (returnToHand)
                {
                    tip.GetComponent<Collider>().enabled = false;
                    body.GetComponent<Collider>().enabled = false;
                    transform.position = Vector3.Lerp(transform.position, playerSpearPos.position, 0.5f);
                    if (Vector3.Distance(transform.position, playerSpearPos.position) < 0.25f) {
                        returnToHand = false;
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    public void Throw()
    {
        isThrown = true;
        isHeld = false;
        transform.parent = null;
        velocity = transform.forward * throwSpeed;
    }

    public void Stick(Transform _p)
    {
        landPos = transform.position;
        transform.parent = _p;
        isThrown = false;
        Debug.Log("what");
    }
}
