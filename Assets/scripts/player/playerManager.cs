using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    public Transform enemyTarget;
    public GameObject shield;
    public float shieldcharge = 100.0f;
    public float moveSpeed = 40.0f;
    public float horizontalMove = 0.0f;

    public Transform spawnTransform;
    public GameObject bullet;

    [Range(0.0f,7.0f)]
    public float bulletSpeed = 0.0f;



    float doubleClickDelay_L = 0.3f;
    int totalLeftKey = 0;

    float doubleClickDelay_R = 0.3f;
    int totalRightKey = 0;

    GameObject activeObject;



    
    // Start is called before the first frame update
    void Start()
    {
        shield.SetActive(false);
    }

    void DoubleKeyPress() {
        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)  ) 
        {
                horizontalMove *=2.5f;
        }
        if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W) )
        {
                if ( !Input.GetKey(KeyCode.A)) {
                horizontalMove *=2.5f;
                }
        }

    }


    // Update is called once per frame
    void Update()
    {
        // to look at the enemy object, which is placed center
        transform.LookAt(enemyTarget);

        horizontalMove = moveSpeed * Time.deltaTime* Input.GetAxis("Horizontal");
        DoubleKeyPress();
        gameObject.transform.RotateAround(enemyTarget.transform.position ,Vector3.up,horizontalMove);
       //  Debug.Log(Input.GetAxis("Horizontal"));


        //shield;
             if ( Input.GetMouseButton(1)) { // right button pressed.
                
                if ( shieldcharge >= 1) {
                        shield.SetActive(true);
                        shieldcharge -=Time.deltaTime * 30;  // decreasing
                } else {
                         shield.SetActive(false);
                }

             }else {
                shield.SetActive(false); // right button released.
                if ( shieldcharge < 100) {
                        shieldcharge +=Time.deltaTime * 30; // increasing;
                }
             }   



        if(Input.GetKeyDown(KeyCode.Space)) {

                activeObject = GameObject.Find("protagonist_bullet(Clone)");

                if ( activeObject == null) {
                Instantiate(bullet,spawnTransform.position, spawnTransform.rotation);
                }

        }



    }

    //bullet spawner;

    /* rotaiton using waves*/
    // void circularMotion( float frequency, float x_amplitude, float z_amplitude) {
    //     float x = Mathf.Cos(Time.time* frequency) * x_amplitude;
    //     float y = 0.5f; // lock it on y axis
    //     float z =  Mathf.Sin(Time.time* frequency) * z_amplitude;
    //     transform.position = new Vector3(x,y,z);
    // }

}
