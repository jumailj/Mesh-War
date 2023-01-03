using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class protagonistManager : MonoBehaviour
{
    public Transform enemyTarget;
    public GameObject shield;
    public float shieldcharge = 100.0f;
    public float moveSpeed = 40.0f;
    public float horizontalMove = 0.0f;

    public Transform spawnTransform;
    public GameObject bullet;

    [Range(0.0f,7.0f)]
    public float bulletSpeed = 10.0f;

    GameObject activeObject;
    
    private ProtagonistController protagonistController;

   private void Awake() {
        protagonistController = new ProtagonistController();
   }


   private void OnEnable() {
        protagonistController.Enable();
   }

   private void OnDisable() {
        protagonistController.Disable();
   }


    
    // Start is called before the first frame update
    void Start()
    {
        shield.SetActive(false);
    }

        void Update()
    {  
        transform.LookAt(enemyTarget);

        HorizontalMove(moveSpeed, 1.5f);
        Fire();
        Shield_();
        
        gameObject.transform.RotateAround(enemyTarget.transform.position ,Vector3.up,horizontalMove);
    }


    void HorizontalMove( float movespeed_, float speedFactor ) {
        float move = protagonistController.Player.move.ReadValue<float>();
        float HorizontaDouble_L = protagonistController.Player.move_Double_L.ReadValue<float>();
        float HorizontaDouble_R = protagonistController.Player.move_Dobule_R.ReadValue<float>();

        horizontalMove = move * movespeed_ * Time.deltaTime;

        if (HorizontaDouble_L != 0 || HorizontaDouble_R != 0) 
                horizontalMove*=speedFactor;          
    }

    void Fire() {
        float fire = protagonistController.Player.Fire.ReadValue<float>();
        if(fire != 0) 
        {
                activeObject = GameObject.Find("protagonistBullet(Clone)");
                
                if ( activeObject == null) 
                {
                Instantiate(bullet,spawnTransform.position, spawnTransform.rotation);
                }
        }
    }

    void Shield_() {
        float shield_key = protagonistController.Player.Shield.ReadValue<float>();

             if ( shield_key != 0) {
                
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
}
