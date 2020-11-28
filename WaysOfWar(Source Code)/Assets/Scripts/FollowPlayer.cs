using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    private Vector3 offset = new Vector3(0,3,-6);
    public float yRotate = 80;
    


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraControl();  
    }


  //Camera contols
 void CameraControl()
    {
        transform.position = Player.transform.position + offset;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(Player.transform.position, Vector3.up, -50 * Time.deltaTime);
            offset = transform.position - Player.transform.position;
            
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(Player.transform.position, Vector3.up, 50 * Time.deltaTime);
            offset = transform.position - Player.transform.position;
        }
      
      
        


        transform.position = Player.transform.position + offset;

    }

}

