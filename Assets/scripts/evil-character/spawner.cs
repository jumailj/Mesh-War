using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Transform SpawnTransform;
    public GameObject bullet;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.red);

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
                Instantiate(bullet, SpawnTransform.position, SpawnTransform.rotation);
        }
     
    }
}
