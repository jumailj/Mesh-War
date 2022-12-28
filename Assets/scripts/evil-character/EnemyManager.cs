using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    // it is used to get the postion&rotation to spawn a bullet
    public Transform SpawnTransform;

    //bullet prefab object;
    public GameObject bullet;

        //set bullet speed
    [Range(0.0f,7.0f)]
    public float bulletSpeed = 0.0f;

    [Range(1,50)]
    public float firingTime = 5.0f;
    [Range(1,50)]
    public float holdingTime = 10.0f;

    [Range(1,40)]
    public  int bulletPerSeconds = 2;

    // firing;
     bool isNumberGenerated = false;
     List<Int32> firingAnagleList = new List<Int32>();

    void Start() {
          Application.targetFrameRate = 500;
    }

    void Update()
    {

        // object rotation;
        //draw Red ray(Debug)
        Vector3 forwardDistance = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forwardDistance, Color.red);

        /* 
        rotation;
        dividing 2 consecutive rotation into 2 part. 
        first part is using positive  0.0 ->  0.9 -> 0.0,[180+180] which is known as firing part
        second part is using negative 0.0 -> -0.9 -> 0.0,[180+180] which is known as holding part 
        */

        //Debug.Log(transform.localRotation.y);
        // if the rotation angle is between 0 and 9 it's on firing state
        if (transform.localRotation.y >= 0) {
                RotateObject(firingTime);
                FireProjitile();
                
            //Debug.Log(count);

        // if the rotation angle is between 0 and -9 it's on holding state
        } else if ( transform.localRotation.y < 0) {
                isNumberGenerated = false;
                firingAnagleList.Clear();
                RotateObject(holdingTime);
        }
    }


    // generate random firing angles and fire.
    void FireProjitile() {
        // random number generator;
        System.Random rnd = new System.Random();
        int randvalue = 0;

        // generate random number equal to firingtime(seconds); and add it to firingAngleList;
         /* numbergenerated(bool) reset on next Holding time; to avoid generating on each steps*/
         if ( isNumberGenerated!= true) {
                for (int j = 1; j <= bulletPerSeconds; j++)
                {
                    randvalue = Convert.ToInt32(rnd.NextDouble() * 360);
                    firingAnagleList.Add(randvalue);     
                }
            isNumberGenerated = true;

            foreach ( int i in firingAnagleList) {
         }
         }

         

         int angle = Convert.ToInt32(transform.localRotation.eulerAngles.y);
        if (firingAnagleList.Contains(angle)) 
        {
                // reduce duplicate firing.
                firingAnagleList.Remove(angle);
                // spawn bullets;
                fire();
        }
    }

    void fire() {
        Instantiate(bullet, SpawnTransform.position, SpawnTransform.rotation);
    }

    void RotateObject(float seconds) 
    {
        transform.Rotate(Vector3.up, (360 * Time.deltaTime) / seconds);

       //  Debug.Log((360 * Time.deltaTime) / seconds);
        // multiply 360 by Time.deltatime give us 360 full rotaiton in 1 seconds.
        // dividing it by seconds to reduce the rotation angle in each steps;
    }


}
