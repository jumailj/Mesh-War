using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{


    public GameObject boundary;
    public GameObject protagonist;
    public GameObject antagonist; 


    public TMP_Text labelScore;
    public TMP_Text labelHealth;
    public TMP_Text labelShieldCharger;

    public TMP_Text labelGameOverScore;

    public GameObject mainMenu;
    public GameObject gameOver;

    public Image UiHealthBar;
    public Image UiShieldBar;


    public ProtagonistManager protagonistManager; 
    public AntagonistManager antagonistManager;
    public Boundary _boundary;

    private bool EnableScore = true;
    private int Difficultiy = 0;
    private float BulletPreSecond = 4.0f;



    void Start() {    
        Application.targetFrameRate = 500;
        setDefaultActive();
        
    }

    void setDefaultActive() {
        boundary.SetActive(false);
        protagonist.SetActive(false);
    //     antagonist.SetActive(false); 
        gameOver.SetActive(false);
    }


    public void StartIntro()
    {
        DestroyBullets();
        //yield on a new YieldInstruction that waits for 5 seconds.
     //    yield return new WaitForSeconds(1); 
         StartGame();
    }


    public void StartGame() {
        
     //    Debug.Log("started game ");

        // delete previous bullets;

        // add some delay();
        boundary.SetActive(true);
        protagonist.SetActive(true);
        antagonist.SetActive(true);
    }

    void DestroyBullets() {

        GameObject[] Bullets =  GameObject.FindGameObjectsWithTag("AntagonistBullet");  //returns GameObject[]

        foreach(GameObject bullets in Bullets)
            if (bullets.activeInHierarchy) {
                    Destroy(bullets);
                //    Debug.Log(bullets);
            }          
  }


    public void BackToMainMenu() {
     //    gameOver.SetActive(true);
        mainMenu.SetActive(true);

        setDefaultActive();
    }

    private int LastScore  = 0;
 
    void Update() {

        int score = protagonistManager.score;
    
       if ( (score!= 0) && (score%2) == 0 && EnableScore == true)
        {
            LastScore = score;
            EnableScore = false;
     
            Difficultiy++;
            IncreseDifficulty();

            Debug.Log("increment difficulty");
        }
        else if(LastScore != score)
        {
            EnableScore = true;
        }



        UiHealthBar.fillAmount = _boundary.Health * 0.01f;
        UiShieldBar.fillAmount = protagonistManager.shieldcharge* 0.01f;

        // Debug.Log( "boundary health in decimal " + _boundary.HealthInDecimal);


        labelScore.text  = protagonistManager.score.ToString();
        labelHealth.text = _boundary.Health.ToString();
        labelShieldCharger.text = protagonistManager.shieldcharge.ToString("0");

        if ( _boundary.Health <= 0) {
            // call one time;
            labelGameOverScore.text = protagonistManager.score.ToString();
            protagonistManager.score = 0; // reset score;

            gameOver.SetActive(true);
            boundary.SetActive(false);
            protagonist.SetActive(false);

            _boundary.Health =100;  // for avoid multiple calls;
            _boundary.ResetColor(); // back to white;
        }

    }

    void IncreseDifficulty()
    {
        if(antagonistManager.bulletSpeed <= 2.8)
        {
            antagonistManager.bulletSpeed += 0.03f;
        }

        if(antagonistManager.firingTime >= 1)
        {
            antagonistManager.firingTime -= 0.05f;
        }

        if(antagonistManager.holdingTime >= 2)
        {
            antagonistManager.holdingTime -= 0.05f;
        }

        if (antagonistManager.bulletPerSeconds <= 8)
        {
            BulletPreSecond  += 0.06f;
            antagonistManager.bulletPerSeconds = (int)BulletPreSecond;
        }

 //       Debug.Log("Bullet Spedd: " + antagonistManager.bulletSpeed);
 //       Debug.Log("firing dealy: " + antagonistManager.firingTime);
 //       Debug.Log("holding time: "+ antagonistManager.holdingTime);
 //       Debug.Log("bullet per second: "+ BulletPreSecond);

    }


}



