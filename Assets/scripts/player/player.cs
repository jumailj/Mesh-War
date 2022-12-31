using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Transform enemyTarget;
    public GameObject shield;
    public float shieldcharge = 100.0f;
    public float moveSpeed = 40.0f;
    public float horizontalMove = 0.0f;


    float doubleClickDelay_L = 0.3f;
    int totalLeftKey = 0;

    float doubleClickDelay_R = 0.3f;
    int totalRightKey = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        shield.SetActive(false);
    }

    void DoubleKeyPress() {
        // left-double control;
        if ( Input.GetKeyDown(KeyCode.A)){
                totalLeftKey += 1;    
        }
        if ( totalLeftKey == 1) {
                doubleClickDelay_L -= Time.deltaTime;
                if ( doubleClickDelay_L <= 0) {
                        totalLeftKey-= 1;
                        doubleClickDelay_L = 1.0f;
                }
        }
        // Debug.Log(totalLeftKey);

        if ( totalLeftKey == 2) {
                horizontalMove *= 2.0f;
        }

        if ( totalLeftKey == 2 && (Input.GetKeyUp(KeyCode.A))) {
                totalLeftKey = 0;
        }


        // right-double control;
        if ( Input.GetKeyDown(KeyCode.D)){
                totalRightKey += 1;    
        }
        if ( totalRightKey == 1) {
                doubleClickDelay_R -= Time.deltaTime;
                if ( doubleClickDelay_R <= 0) {
                        totalRightKey-= 1;
                        doubleClickDelay_R = 1.0f;
                }
        }
        Debug.Log(totalRightKey);

        if ( totalRightKey == 2) {
                horizontalMove *= 2.0f;
        }

        if ( totalRightKey == 2 && (Input.GetKeyUp(KeyCode.D))) {
                totalRightKey = 0;
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
    }

    /* rotaiton using waves*/
    // void circularMotion( float frequency, float x_amplitude, float z_amplitude) {
    //     float x = Mathf.Cos(Time.time* frequency) * x_amplitude;
    //     float y = 0.5f; // lock it on y axis
    //     float z =  Mathf.Sin(Time.time* frequency) * z_amplitude;
    //     transform.position = new Vector3(x,y,z);
    // }

}
