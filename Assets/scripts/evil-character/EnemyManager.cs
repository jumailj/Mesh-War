using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    // set enemy object rotation 
    [Range(50.0f, 180.0f)]
    public float RotationSpeed = 50.0f;

    //set bullet speed
    [Range(10,50)]
    public float bulletSpeed = 10.0f;

    // it is used to get the postion&rotation to spawn a bullet
    public Transform SpawnTransform;

    //bullet prefab object;
    public GameObject bullet;

    // Update is called once per frame
    void Update()
    {
        // object rotation;
        transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime); 

        //draw Red ray(Debug)
        Vector3 forwardDistance = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forwardDistance, Color.red);

        //spawn bullet;
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Instantiate(bullet, SpawnTransform.position, SpawnTransform.rotation);
        }
    }
}
