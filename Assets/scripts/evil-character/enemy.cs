using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
     [Range(20, 180)]
     public float RotationSpeed = 30f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime); 
    }
}
