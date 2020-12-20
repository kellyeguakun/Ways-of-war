using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private PlayerController playerControllerScript;


    // Start is called before the first frame update
    public int Dmg = 20;
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

   //When enemy hits player
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player"&& playerControllerScript.BlockActive==false)
        {
            playerControllerScript.TakeDamage(Dmg);
            StartCoroutine(playerControllerScript.GetComponent<PlayerController>().GotHit());
            Debug.Log("You took Dmg Ouch ");
        }
    }

}
