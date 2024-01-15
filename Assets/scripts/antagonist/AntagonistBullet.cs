using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class AntagonistBullet : MonoBehaviour
{
    /* AntagonistBullet is a prefab. can't drop referance to prefab.*/

            ParticleSystem sparkParticle;
            AudioManager audioManager;

            GameObject antagonistObject;
            AntagonistManager antagonistManager;

            GameObject protagonisObject;
            ProtagonistManager protagonistManager;

    void Start()
    {
        sparkParticle = GameObject.FindGameObjectWithTag("SparkParticles").GetComponent <ParticleSystem>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

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
            // play explosive particle effects
            sparkParticle.transform.position = this.transform.position;
            sparkParticle.Play();

            // play sound effects
            audioManager.Stop("blast"); // stops if any previous blast is playing.
            audioManager.Play("blast");


            protagonistManager.score += 1; 
            Destroy(this.gameObject);
            
        }   

        if (other.tag == "ProtagonistBullet")
        {
            // play sound effects
            audioManager.Stop("blast"); // stops if any previous blast is playing.
            audioManager.Play("blast");

            // play explosive particle effects
            sparkParticle.transform.position = this.transform.position;
            sparkParticle.Play();
        }
    }
 }
    