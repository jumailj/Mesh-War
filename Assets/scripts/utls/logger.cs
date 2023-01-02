using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System;


public class logger : MonoBehaviour
{

    public protagonistManager player;

    public GameObject playerObject;

    public Text fpsText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int val = (int)(1.0f / Time.smoothDeltaTime);
        string player_shield = Convert.ToInt32(player.shieldcharge).ToString();
        string playerSpeed = (player.horizontalMove*10).ToString();

        fpsText.text =  "Fps : " + val.ToString() + "\n" + "Shield: " + player_shield + "\n" +"Player Speed: "+playerSpeed; 

    }
}
