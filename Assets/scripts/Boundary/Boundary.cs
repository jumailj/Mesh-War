using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{

    public PlayfabManager playfabManager;
    public ProtagonistManager protagonistmanager;
    Color fullHealthColor = Color.white;
    Color zeroHealthColor = Color.red;

    public float Health = 100.0f;
    public int numberOfBullets = 5;
    public Material boundrayMat;

    float lerp = 0.0f;

    void Start() {
             gameObject.SetActive(false);
             // reset boundary color to white. before game start. to avoid last section Material save.
             boundrayMat.SetColor("_EmissionColor", fullHealthColor);
    }
    
    void Update()
    {
        if (Health <= 0 ) {
            boundrayMat.SetColor("_EmissionColor", fullHealthColor);
            playfabManager.SendScoreBoard(protagonistmanager.score);
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
