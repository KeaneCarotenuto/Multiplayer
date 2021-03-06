using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple dont destroy on load script
/// </summary>
public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (GameObject.Find(gameObject.name) != gameObject)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
