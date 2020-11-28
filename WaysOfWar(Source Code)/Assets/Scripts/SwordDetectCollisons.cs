using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDetectCollisons : MonoBehaviour
{
    private  Enemy EnemyScript;
    private PlayerController playerControllerScript;
    

    // Start is called before the first frame update
    void Start()
    {


        



    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Enemy(Clone)")!= null)
        {
          EnemyScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
            

        }
        
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void AnimAttack()
    {
        if (playerControllerScript.Attack() == true)
        {
           
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (playerControllerScript.Attack() == true)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                
                EnemyScript.TakeDamage(20);
                Debug.Log("It works ");
            }
        }
     

    }

}
