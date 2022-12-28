using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    public Transform enemyTarget;

    public float moveSpeed = 40.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // to look at the enemy object, which is placed center
        transform.LookAt(enemyTarget);

        Vector2 Mouse_pos = Input.mousePosition;




        if ( Input.GetKey(KeyCode.A)) {
        gameObject.transform.RotateAround(enemyTarget.transform.position ,Vector3.up,moveSpeed * Time.deltaTime);
        } else if ( Input.GetKey(KeyCode.D)) {
            gameObject.transform.RotateAround(enemyTarget.transform.position ,Vector3.up,-moveSpeed * Time.deltaTime);
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
