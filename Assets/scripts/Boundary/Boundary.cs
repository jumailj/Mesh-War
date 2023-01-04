using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{


    Color fullHealthColor = Color.white;
    Color zeroHealthColor = Color.red;

    public float Health = 100.0f;
    public int numberOfBullets = 5;
    public Material boundraymat;

    float lerp = 0.0f;
    
    void Update()
    {
        if (Health <= 0 ) {
             gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    { 
       if ( other.gameObject.tag == "enemyBullet") {
            Destroy(other.gameObject);
            Health -= 100/numberOfBullets;
            lerp = Health*0.01f;
            boundraymat.SetColor("_EmissionColor",Color.Lerp(zeroHealthColor, fullHealthColor, lerp));
       }
       
    }
}
