using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistBullet : MonoBehaviour
{

    GameObject Protagonist;


    ProtagonistManager protagonistmanager;

    void Start()
    {
        Protagonist = GameObject.FindWithTag("Protagonist");
        protagonistmanager = Protagonist.GetComponent<ProtagonistManager>();   
    }

    void Update()
    {
         transform.Translate(Vector3.forward* protagonistmanager.bulletSpeed * Time.deltaTime);    
         transform.Rotate(Vector3.forward* protagonistmanager.bulletSpeed * 200 * Time.deltaTime);
    }
  
    private void OnTriggerEnter(Collider other)
    {    
        if (other.tag == "Antagonist") {
            Destroy(this.gameObject);
            
        }

        if (other.tag == "AntagonistBullet") {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            protagonistmanager.score += 1;    
        }

    }
}
