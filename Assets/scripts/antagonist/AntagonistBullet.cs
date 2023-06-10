using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntagonistBullet : MonoBehaviour
{        
            GameObject antagonist;
            AntagonistManager antagonistManager;

    void Start()
    {
        antagonist = GameObject.FindWithTag("Antagonist");
        antagonistManager = antagonist.GetComponent<AntagonistManager>();
        //  Debug.Log(enemymanager.bulletSpeed) ;
    }

    void Update()
    {
         transform.Translate(Vector3.forward* antagonistManager.bulletSpeed * Time.deltaTime);    
         transform.Rotate(Vector3.forward* antagonistManager.bulletSpeed * 200 * Time.deltaTime);
        // Debug.Log("bullet speed: " + Enemymanager.bulletSpeed);  

    }

    // collision;
    private void OnTriggerEnter(Collider other)
    {    
        if (other.tag == "bulletDespawner") {
                Destroy(this.gameObject);
        }

        if (other.tag == "ProtagonistShield") {
                Destroy(this.gameObject);
        }  

        //  if (other.gameObject.tag == "Boundry") { 
        //       //     Destroy(this.gameObject);
        // }  
    }
 }
    