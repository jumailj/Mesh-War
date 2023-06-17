using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System;


public class logger : MonoBehaviour
{

    public ProtagonistManager protagonist;
    public AntagonistManager antagonist;

    public GameObject playerObject;
    public Text fpsText;


    void Update()
    {
        int val = (int)(1.0f / Time.smoothDeltaTime);
        string player_shield = Convert.ToInt32(protagonist.shieldcharge).ToString();
        string playerSpeed = (protagonist.horizontalMove*10).ToString();

        fpsText.text = "Fps : " + val.ToString() + "\n\n"
            + "Protagonist:\n"
            + "Shield: " + player_shield
            + "\n" + "Player Speed: " + playerSpeed
            + "\nBulletSpeed: " + protagonist.bulletSpeed.ToString()
            + "\n\n"
            + "Antagonist:\n"
            + "BulletSpeed: " + antagonist.bulletSpeed.ToString()
            + "\n"
            + "Firing Time: " + antagonist.firingTime.ToString()
            + "\n"
            + "Holding Time: "+ antagonist.holdingTime.ToString()
            + "\n"
            + "Bullet per rotation: "+ antagonist.bulletPerRotation.ToString();
            

    }
}
