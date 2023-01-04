using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{



    public float Health = 100.0f;
    public int numberOfBullets = 5;
    public Material boundraymat;


    
    //damage = 100/numberOfBullets  || 100/5 = 20
    // health -= damage;

    void Start()
    {
        
    }

    void Update()
    {
        if (Health <= 0 ) {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    { 

       Debug.Log(" on trigger other : " + other.gameObject.tag);

       if ( other.gameObject.tag == "enemyBullet") {
            Debug.Log("health: " + Health);
            Destroy(other.gameObject);
            Health -= 100/numberOfBullets;
       }

    }


}
