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
        labelScore.text  = protagonistManager.score.ToString();
        labelHealth.text = _boundary.Health.ToString();
        labelShieldCharger.text = protagonistManager.shieldcharge.ToString("0");

        if ( _boundary.Health <= 0) {
            // call one time;
            labelGameOverScore.text = protagonistManager.score.ToString();

            gameOver.SetActive(true);
            boundary.SetActive(false);
            protagonist.SetActive(false);


            _boundary.Health =100;  // for avoid multiple calls;
            _boundary.ResetColor(); // back to white;
        }

    }

}
