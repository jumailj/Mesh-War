using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{        
            GameObject enemy;
            EnemyManager enemymanager;

    void Start()
    {
        enemy = GameObject.Find("evil_character");
        enemymanager = enemy.GetComponent<EnemyManager>();
        //  Debug.Log(enemymanager.bulletSpeed) ;
    }

    void Update()
    {
         transform.Translate(Vector3.forward* enemymanager.bulletSpeed * Time.deltaTime);    
        // Debug.Log("bullet speed: " + Enemymanager.bulletSpeed);

        
    }

    // collision;
    private void OnTriggerEnter(Collider other)
    {    
        if (other.tag == "bulletDespawner") {
                  Destroy(this.gameObject);
        }
    }

 }
    

