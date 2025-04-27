using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem.LowLevel;



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

    public PlayfabManager playfabManager;

    public AudioManager audioManager;
    public Animator boundaryAnimator;

    private bool EnableScore = true;
    private float BulletPreSecond = 4.0f;



   public enum GameLevelState {idel = 0, level1 = 1, level2= 2, level3= 3 };
   public  GameLevelState gameLevelState = GameLevelState.idel;


    void Start() {    
        Application.targetFrameRate = 500;

        // boundary-Object, Protagonist-Object, GameOver-UL should be disabled;
        setDefaultActive();
        ChangeBoundaryState(GameLevelState.idel);
    }

    void setDefaultActive() {
        boundary.SetActive(false);
        protagonist.SetActive(false);
      //antagonist.SetActive(false); 
        gameOver.SetActive(false);
    }


    public void StartIntro()
    {
        // remove all bullets from pervious section, and start game
        DestroyBullets();
        StartLevel();
    }


    public void StartLevel() {

        ChangeBoundaryState(GameLevelState.level1);


        boundary.SetActive(true);
        protagonist.SetActive(true);
        antagonist.SetActive(true);
    }

    void DestroyBullets() {

        GameObject[] Bullets =  GameObject.FindGameObjectsWithTag("AntagonistBullet");  //returns GameObject[]
        foreach(GameObject bullets in Bullets)
            if (bullets.activeInHierarchy) {
                    Destroy(bullets);
            }          
    }


    public void BackToMainMenu() {

        mainMenu.SetActive(true);
        setDefaultActive();


        // reset Antagonist Properties, for next-start
        antagonistManager.bulletSpeed = 1.0f;
        antagonistManager.firingTime = 6.0f;
        antagonistManager.holdingTime = 5.0f;
        antagonistManager.bulletPerRotation = 4;
        BulletPreSecond = 4.0f;


        // update scoreboard;
        playfabManager.GetScoreboard();
    }

    private int LastScore  = 0;
 
    void Update() {
        int score = protagonistManager.score;

        if ( (score!= 0) && (score%2) == 0 && EnableScore == true)
        {
            LastScore = score;
            EnableScore = false;
  
            IncreseDifficulty();


            switch(score) //change difficulty relative to score, should be even number;
            {
                // first level should be change in start;
                case 6:  // level-2
                    ChangeBoundaryState(GameLevelState.level2);
                    break;
                case 12: // level-3
                    ChangeBoundaryState(GameLevelState.level3);
                    break;
            }

        }
        else if(LastScore != score)
        {
            EnableScore = true;
        }

        UiHealthBar.fillAmount = _boundary.Health * 0.01f;
        UiShieldBar.fillAmount = protagonistManager.shieldcharge* 0.01f;


        labelScore.text  = protagonistManager.score.ToString();
        labelHealth.text = _boundary.Health.ToString();
        labelShieldCharger.text = protagonistManager.shieldcharge.ToString("0");

        if ( _boundary.Health <= 0) {
            gameLevelState = GameLevelState.idel;
            ChangeBoundaryState(GameLevelState.idel);
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

        if (antagonistManager.bulletPerRotation <= 8)
        {
            BulletPreSecond  += 0.06f;
            antagonistManager.bulletPerRotation = (int)BulletPreSecond;
        }

    }


    void ChangeBoundaryState(GameLevelState state)
    {
        gameLevelState = state; // change globe state;
        switch (state)
        {
            case GameLevelState.idel: // idel; // refer to menu state.
                ChangeAudio("divide-by-zero", "menu");
                ChangeAnimation(boundaryAnimator, "idel");

                break;
            case GameLevelState.level1:
                    ChangeAudio("menu", "galaxy-nauts");
                    ChangeAnimation(boundaryAnimator, "level-1");
                break;
            case GameLevelState.level2:
                    ChangeAudio("galaxy-nauts", "net-bots");
                    ChangeAnimation(boundaryAnimator, "level-2");
                break;
            case GameLevelState.level3:
                    ChangeAudio("net-bots", "divide-by-zero");
                    ChangeAnimation(boundaryAnimator, "level-3");
                break;
            default:
                Debug.LogError("unknown gamestate state");
                break;
        }
    }

    void ChangeAnimation(Animator animator, String animationString)
    {
        animator.SetFloat(animationString, 1.0f);
        animator.Play(animationString);
    }

    void ChangeAudio(String previous, String next)
    {
        audioManager.Stop(previous);
        audioManager.Play(next);
    }
}
