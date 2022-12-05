using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [Range(1f, 20f)]
    public float Bulletspeed = 10f;

    public GameObject parent;
    void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(Vector3.forward* Bulletspeed * Time.deltaTime);    
        Debug.Log("bullet speed: " + Bulletspeed);
    }
}
