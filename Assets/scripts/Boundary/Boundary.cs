using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{

    public float Health = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
                    Debug.Log("health: " + Health);
        
    }


    void OnCollisionEnter(Collision other)
    {
        Debug.Log("other : " + other.gameObject.tag);
        if (other.gameObject.tag == "enemyBullet") {
            Debug.Log("collided");
                 Destroy(other.gameObject);
                Destroy(this.gameObject);
        }  
    }



}
