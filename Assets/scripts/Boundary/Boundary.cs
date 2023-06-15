
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public PlayfabManager playfabManager;
    public ProtagonistManager protagonistmanager;

    private float GroundColorDelay = 0.5f;
    private bool collide = false;


    Color fullHealthColor = Color.white;
    Color zeroHealthColor = Color.red;


    public float Health = 100.0f;

    // tototal number of bullets to reduce the health from 100 to 0;
    // 100/5 => 20 bullets;
    public int numberOfBullets = 5;

    public Material groundMat;
    public Material boundrayMat;


    void Start() {
             // reset boundary color to white. before game start. to avoid last section Material save.
             ResetColor();
    }

    public void ResetColor() {
        // reset color to white.
            boundrayMat.SetColor("_EmissionColor", fullHealthColor);
        //reset color to black for ground.
            groundMat.SetColor("_Color", Color.black);
    }
    
    void Update()
    {
        if(collide == true )
        {
            // pulsing color from black to red.
            GroundColorDelay -= Time.deltaTime;
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

            Health -= 100/ numberOfBullets;
          
            if ( Health <= 0)
            {
                boundrayMat.SetColor("_EmissionColor", fullHealthColor);
                playfabManager.SendScoreBoard(protagonistmanager.score);         
                gameObject.SetActive(false);
            }

            boundrayMat.SetColor("_EmissionColor",Color.Lerp(zeroHealthColor, fullHealthColor, Health * 0.01f));


            // if collide with AntagonisthBullet
            // animate ground color;
            collide = true;
       }
 
    }
}
