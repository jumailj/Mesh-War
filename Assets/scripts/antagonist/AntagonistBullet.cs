using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntagonistBullet : MonoBehaviour
{        

    /* AntagonistBullet is a prefab. dan't drop referance to prefab.*/
            GameObject antagonistObject;
            AntagonistManager antagonistManager;


            GameObject protagonisObject;
            ProtagonistManager protagonistManager;


    void Start()
    {
        antagonistObject = GameObject.FindWithTag("Antagonist");
        antagonistManager = antagonistObject.GetComponent<AntagonistManager>();

        if(GameObject.FindWithTag("Protagonist")!= null)
        {
            protagonisObject = GameObject.FindWithTag("Protagonist");
            protagonistManager = protagonisObject.GetComponent<ProtagonistManager>();
        }  
    }

    void Update()
    {
         transform.Translate(Vector3.forward* antagonistManager.bulletSpeed * Time.deltaTime);    
         transform.Rotate(Vector3.forward* antagonistManager.bulletSpeed * 200 * Time.deltaTime);
    }

    // collision;
    private void OnTriggerEnter(Collider other)
    {    
        if (other.tag == "bulletDespawner") 
        {
                Destroy(this.gameObject);
        }

        if (other.tag == "ProtagonistShield")
        {     
                protagonistManager.score += 1;      
               Destroy(this.gameObject);
            
        }   
    }
 }
    