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

    public float lifetime = 10.0f;

    public float spawnTime = 0;

    Vector3 landPos;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        
    }

    private void OnLevelWasLoaded()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isServer)
        {
            transform.localScale -= new Vector3(0.1f * Time.deltaTime, 0.1f * Time.deltaTime, 0);

            if (Time.time - spawnTime >= lifetime)
            {
                NetworkServer.UnSpawn(gameObject);
                NetworkServer.Destroy(gameObject);
            }

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
                            FlipVel();
                            break;
                        }
                    }
                    else
                    {
                        if (_hit.transform.root.GetComponent<PlayerMovement>())
                        {
                            FlipVel();
                        }
                        else
                        {
                            Stick();
                        }

                        break;
                    }
                }
            }
        }
    }

    private void FlipVel()
    {
        velocity *= -Random.Range(0.8f, 0.95f);
    }

    public void Throw()
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

        isThrown = true;
        isHeld = false;
        transform.parent = null;
        velocity = transform.forward * throwSpeed;
    }

    public void Stick()
    {
        landPos = transform.position;
        transform.parent = null;
        isThrown = false;



        if (isServer)
        {
            RpcUpdateAll(transform);
        }

        //if (true)
        //{
        //    transform.localScale *= 10;
        //    CmdUpdateAll(transform);
        //}
    }

    [ClientRpc]
    void RpcUpdateAll(Transform _trans)
    {
        transform.position = _trans.position;
        transform.rotation = _trans.rotation;
    }

    [Command(requiresAuthority = false)]
    void CmdUpdateAll(Transform _trans)
    {
        GameObject s = Instantiate(spearPrefab, _trans.position, _trans.rotation, null);
        NetworkServer.Spawn(s);

        s.transform.position = _trans.position;
        s.transform.rotation = _trans.rotation;
        s.transform.localScale = _trans.localScale;

        RpcUpdateAll(_trans);
    }
}
