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

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            playerControllerScript.TakeDamage(Dmg);
            Debug.Log("You took Dmg Ouch ");
        }
    }

}
