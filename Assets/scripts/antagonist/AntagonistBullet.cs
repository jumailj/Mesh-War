using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

            string ObjName;
            float newBulletSpeed = 1.0f;

    void Start()
    {
        ObjName = this.gameObject.name;
        Debug.Log(ObjName);

        sparkParticle = GameObject.FindGameObjectWithTag("SparkParticles").GetComponent <ParticleSystem>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        antagonistObject = GameObject.FindWithTag("Antagonist");
        antagonistManager = antagonistObject.GetComponent<AntagonistManager>();

        if(GameObject.FindWithTag("Protagonist")!= null)
        {
            protagonisObject = GameObject.FindWithTag("Protagonist");
            protagonistManager = protagonisObject.GetComponent<ProtagonistManager>();
        }

        // determine which bullet is hitting.
        if (ObjName == "AntagonistBullet1(Clone)") // level-2 bullet
        {
            newBulletSpeed = antagonistManager.bulletSpeed * 1f; //todo remove later.
            Debug.Log("senond bullet");
        }
        else // level-1 bullet
        { 
            newBulletSpeed = antagonistManager.bulletSpeed;
        }

    }

    void Update()
    {
        transform.Translate(Vector3.forward * newBulletSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward* newBulletSpeed * 200 * Time.deltaTime);
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
    