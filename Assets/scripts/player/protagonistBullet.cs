using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class protagonistBullet : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject player;
    playerManager playermanager;

    
    void Start()
    {
        player = GameObject.FindWithTag("player");
        playermanager = player.GetComponent<playerManager>();   

        Debug.Log("player bullet init");  

        Debug.Log(player);
    }

    // Update is called once per frame
    void Update()
    {
         transform.Translate(Vector3.forward* playermanager.bulletSpeed * Time.deltaTime);    
         transform.Rotate(Vector3.forward* playermanager.bulletSpeed * 200 * Time.deltaTime);
         Debug.Log("bullet speed: " + playermanager.bulletSpeed);  

    }
  
    private void OnTriggerEnter(Collider other)
    {    
        if (other.tag == "enemy") {
                Destroy(this.gameObject);
                
        }

         if (other.tag == "enemyBullet") {
                Destroy(other.gameObject);
                Destroy(this.gameObject);
        }
    }
}
