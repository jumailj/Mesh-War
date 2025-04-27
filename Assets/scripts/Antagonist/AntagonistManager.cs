using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class AntagonistManager : MonoBehaviour
{
    // it is used to get the postion&rotation to spawn a bullet
    public Transform SpawnTransform;

    //bullet prefab object;
    public GameObject bullet;
    
    public Animator animator;

    private float time = 0.0f;
    private float value = 0.0f;


    //Antagonist-Properties.
    [Tooltip("Bullet Speed")]
    [Range(0.0f,7.0f)]
    public float bulletSpeed = 1.0f;

    [Tooltip("Firing time in Seconds")]
    [Range(1,10)]
    public float firingTime = 6.0f;

    [Tooltip("Holding time in Seconds")]
    [Range(1,10)]
    public float holdingTime = 5.0f;

    [Tooltip("Number of Bullets per Rotation.")]
    [Range(1,20)]
    public int bulletPerRotation = 4;


    // firing;
     bool isNumberGenerated = false;
     List<Int32> firingAnagleList = new List<Int32>();


    void Update()
    {

        // object rotation;
        //draw Red ray(Debug)
        Vector3 forwardDistance = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forwardDistance, Color.red);

        /* 
        rotation;
        dividing 2 consecutive rotation into 2 part. 
        first part is using positive  0.0 ->  0.9 -> 0.0,[180+180] which is known as holding part
        second part is using negative 0.0 -> -0.9 -> 0.0,[180+180] which is known as firing part 
        */

        //(transform.localRotation.y);

        // if the rotation angle is between 0 and -9 it's on firing state
        if (transform.localRotation.y < 0) {

                time = 0.0f;
                RotateObject(firingTime);
                FireProjitile();
                FiringAnimation();

        }
        // if the rotation angle is between 0 and 9 it's on holding state   
        else if ( transform.localRotation.y >= 0) {

                isNumberGenerated = false; 
                firingAnagleList.Clear();
                RotateObject(holdingTime);
                IdelAnimation();
        }
    }


    // generate random firing angles and fire.
    void FireProjitile() {
        // random number generator;
        System.Random random = new System.Random();

        // generate random number equal to firingtime(seconds); and add it to firingAngleList;
         /* numbergenerated(bool) reset on next Holding time; to avoid generating on each steps*/
         if ( isNumberGenerated!= true) { 
                for (int j = 1; j <= bulletPerRotation; j++)
                {
                    int randvalue = random.Next(0, 361);
                    firingAnagleList.Add(randvalue);     
                }

            isNumberGenerated = true;
         }

        // get current angle from object transfrom and check the firstAngle List;
        int angle = Convert.ToInt32(transform.localRotation.eulerAngles.y);
        if (firingAnagleList.Contains(angle))  
        {
                // remove duplicate firing angle.
                firingAnagleList.Remove(angle);

                // spawn bullets;
                Instantiate(bullet, SpawnTransform.position, SpawnTransform.rotation);
        }
    }


    void RotateObject(float seconds) 
    {
        transform.Rotate(Vector3.up, (360 * Time.deltaTime) / seconds);

        // ((360 * Time.deltaTime) / seconds);
        // multiply 360 by Time.deltatime give us 360 full rotaiton in 1 seconds.
        // dividing it by seconds, to rotate one full cycle in given seconds.

    }

    //firing-animation, which start after the idel state.
    void FiringAnimation()
    {
        if (animator != null)
        {
            animator.CrossFade("Start_Firing", 0);
        }
    }

    // ideal animation, plays when antagonist after firing state.
    void IdelAnimation()
    {
        if (animator != null)
        {
            // blending between stop-firing state and idel state.
            animator.SetFloat("Idel", value);
            // play stop-firing(blend) animation.(transitions from stop-firing to idel)
            animator.Play("Stop_Firing");
            value = Mathf.Lerp(0, 10, time / 10);
            time += Time.deltaTime;
        }
    }

}
