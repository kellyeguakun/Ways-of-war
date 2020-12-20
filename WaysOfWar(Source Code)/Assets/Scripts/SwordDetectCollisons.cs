using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDetectCollisons : MonoBehaviour
{
    private  Enemy EnemyScript;
    private Boss BossScript;
    private PlayerController playerControllerScript;
    public int Str;
    

    
    void Start()
    {


        



    }

    
    void Update()
    {
        //Calling Other Scripts
        if (GameObject.Find("Enemy(Clone)")!= null)
        {
          EnemyScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
            

        }
        
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        BossScript = GameObject.Find("Boss").GetComponent<Boss>();

        Str = playerControllerScript.Strength;





    }

    void AnimAttack()
    {
        if (playerControllerScript.Attack() == true)
        {
           
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
       //Enemy is hit
        if (playerControllerScript.Attack() == true)
        {
            if (other.gameObject.tag=="Enemy")
            {
                
                EnemyScript.TakeDamage(Str);
                Debug.Log("You Hit some one");
            
            }
           //Boss is hit
            else if (other.gameObject.tag=="Boss")
            {
                Debug.Log("You hit the boss");
                BossScript.TakeDamage(Str);
                
            }
            
        }
     

    }
    

}
