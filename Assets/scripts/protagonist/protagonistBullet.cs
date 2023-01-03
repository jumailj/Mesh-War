using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class protagonistBullet : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject PlayFab;
    GameObject player;


    // Use this for initialization

    PlayfabManager PlayfabManager;
    protagonistManager playermanager;

    void Start()
    {
        PlayFab = GameObject.FindWithTag("PlayFab");
        PlayfabManager = PlayFab.GetComponent<PlayfabManager>(); 

        player = GameObject.FindWithTag("player");
        playermanager = player.GetComponent<protagonistManager>();   

        // Debug.Log("player bullet init");  
        // Debug.Log(player);
    }

    // Update is called once per frame
    void Update()
    {
         transform.Translate(Vector3.forward* playermanager.bulletSpeed * Time.deltaTime);    
         transform.Rotate(Vector3.forward* playermanager.bulletSpeed * 200 * Time.deltaTime);
        //  Debug.Log("bullet speed: " + playermanager.bulletSpeed);  

    }
  
    private void OnTriggerEnter(Collider other)
    {    
        if (other.tag == "enemy") {
            Destroy(this.gameObject);
            playermanager.score += 1;
        }

        if (other.tag == "enemyBullet") {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            Debug.Log(playermanager.score);
            PlayfabManager.SendScoreBoard(playermanager.score);
        }
    }
}
