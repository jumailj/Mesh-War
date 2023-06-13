
using UnityEngine;

public class Boundary : MonoBehaviour
{

    public PlayfabManager playfabManager;
    public ProtagonistManager protagonistmanager;
    public Material groundMat;

    private float GroundColorDelay = 0.5f;
    private bool collide = false;


    Color fullHealthColor = Color.white;
    Color zeroHealthColor = Color.red;

    public float Health = 100.0f;
    // private float HealthInDecimal = 1.0f; 
    public int numberOfBullets = 5;
    public Material boundrayMat;


    void Start() {
             // reset boundary color to white. before game start. to avoid last section Material save.
             ResetColor();

        groundMat.SetColor("_Color", Color.black);
    }

    public void ResetColor() {
            boundrayMat.SetColor("_EmissionColor", fullHealthColor);
    }
    
    void Update()
    {
        if (Health <= 0 ) {
            boundrayMat.SetColor("_EmissionColor", fullHealthColor);
            playfabManager.SendScoreBoard(protagonistmanager.score);
             gameObject.SetActive(false);
        }

        if(collide == true )
        {
            GroundColorDelay -= Time.deltaTime;
            Debug.Log("collided with boundeary... " + GroundColorDelay);
            groundMat.SetColor("_Color", new Color(GroundColorDelay*3.0f, 0.0f, 0.0f, 1.0f));

            if (GroundColorDelay <= 0)
            {
                GroundColorDelay = 0.5f;
                collide = false;
                groundMat.SetColor("_Color", Color.black);
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    { 
       if ( other.gameObject.tag == "AntagonistBullet") {

            Destroy(other.gameObject);     
            Health -= 100/numberOfBullets;
          //   HealthInDecimal = Health*0.01f;

            boundrayMat.SetColor("_EmissionColor",Color.Lerp(zeroHealthColor, fullHealthColor, Health * 0.01f));

            // animate: ground color;
            collide = true;

       }
 
    }
}
