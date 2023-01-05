using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    Color fullHealthColor = Color.white;
    Color zeroHealthColor = Color.red;

    public float Health = 100.0f;
    public int numberOfBullets = 5;
    public Material boundrayMat;

    float lerp = 0.0f;

    void Start() {
             gameObject.SetActive(false);
    }
    
    void Update()
    {
        if (Health <= 0 ) {
             boundrayMat.SetColor("_EmissionColor", fullHealthColor);
             gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    { 
       if ( other.gameObject.tag == "AntagonistBullet") {
            Destroy(other.gameObject);     
            Health -= 100/numberOfBullets;
            lerp = Health*0.01f;

            boundrayMat.SetColor("_EmissionColor",Color.Lerp(zeroHealthColor, fullHealthColor, lerp));
       }
       
    }
}
