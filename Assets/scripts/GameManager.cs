using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    public Boundary _boundary;
    

    void Start() {    
        Application.targetFrameRate = 500;
        setDefaultActive();
   
    }

    void setDefaultActive() {
        boundary.SetActive(false);
        protagonist.SetActive(false);
        antagonist.SetActive(false); 
        gameOver.SetActive(false);
    }


    public IEnumerator StartIntro()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);
         StartGame();

    }


    public void StartGame() {
        Debug.Log("started game ");


        // add some delay();
        boundary.SetActive(true);
        protagonist.SetActive(true);
        antagonist.SetActive(true);
    }


    public void BackToMainMenu() {
     //    gameOver.SetActive(true);
        mainMenu.SetActive(true);

        setDefaultActive();
    }


    
    void Update() {

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




}
