using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Players Variables
    public float speed = 5.0f;
    public float RotateSpeed = 2.0f;
    public float turnspeed = 5.0f;
    private float horizontalInput;
    private float forwardInput;
    private Rigidbody playerRb;
    public float gravityModifier;
    private float Rotate = 3.3f;
    private Animator PlayerAnim;
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthbar;
    public bool GameOver = false;
    public bool isOnGround = true;
    public bool AttackActive = false;
    public bool BlockActive = false;
    public bool hasHealthPotion;
    public bool hasPowerup;
    public bool hasKey;
    public bool PlayerHit;
    private DetectCollisions DetectScript;
    public ParticleSystem explosionParticle;
    public ParticleSystem powerupEmission;
    public TextMeshProUGUI PoweupUI;
    public TextMeshProUGUI Mission3;
    public TextMeshProUGUI Mission2;
    public TextMeshProUGUI GameOverGui;
    public GameObject RestartGui;
    
    public GameObject MainGate;
    public GameObject MainGateIns;
    private HealthBar HealthBarScript;
    public int Strength;
    public GameObject PowerupIndicator;
    public TextMeshProUGUI Done;
    public AudioSource AttackSound;
    public AudioSource JumpSound;








    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        PlayerAnim = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
     
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        Attack();
        Block();
        onDeath();
        

        HealthBarScript = GameObject.Find("HealthBar").GetComponent<HealthBar>();

        PowerupIndicator.transform.position = transform.position + new Vector3(0, 0.5f, 0);

        //Debugging Tool
        if (Input.GetKeyDown(KeyCode.X))
        {
            
            TakeDamage(20);
            StartCoroutine(GotHit());
            Debug.Log("Debug(Hit)");
        }
    }
   
    //ALL Movement of Player
    void movePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

 
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) && GameOver==false)
        {
            PlayerAnim.Play("Run");
            PlayerAnim.SetBool("Run", true);
            PlayerAnim.SetFloat("Speed_f", 1.0f);




        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            PlayerAnim.SetBool("Run", false);



        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            PlayerAnim.SetBool("Run", true);
            PlayerAnim.SetFloat("Speed_f", -1.0f);

        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            PlayerAnim.SetBool("Run", false);
            PlayerAnim.SetFloat("Speed_f", 0.0f);

        }







            //Space Bar Jump
            if (Input.GetKeyDown(KeyCode.Space)&& isOnGround && !GameOver) {
            playerRb.AddForce(Vector3.up * 100, ForceMode.Impulse);
            isOnGround = false;
            PlayerAnim.SetTrigger("Jump_trig");
            JumpSound.Play();
        }

        //Rotation of object
       

        if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow) && !GameOver)
        {
            transform.Rotate(Vector3.up, -Rotate * RotateSpeed*Time.deltaTime, Space.World);
            PlayerAnim.Play("Run");
           

        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            
        }


        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) && GameOver == false)
        {
            transform.Rotate(Vector3.up, Rotate * RotateSpeed * Time.deltaTime, Space.World);
            PlayerAnim.Play("Run");
        }

        else if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow) && GameOver == false)
        {
            transform.Rotate(Vector3.up, -Rotate * RotateSpeed * Time.deltaTime, Space.World);
        }










    }
    public bool Attack()
    {  
        //When you Attack
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Mouse0) && GameOver == false)
        {
            PlayerAnim.SetTrigger("Attack");
            AttackSound.Play();
            AttackActive = true;
        }

        else if(Input.GetKeyUp(KeyCode.J) || Input.GetKeyUp(KeyCode.Mouse0))
        {

            AttackActive = false;
        }

        return (AttackActive);
    }

    public bool Block()
    {
        //When you Block
        if (Input.GetKeyDown(KeyCode.E) && GameOver == false)
        {
            
           
            PlayerAnim.Play("Block");
            BlockActive = true;

        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            BlockActive = false;
           
        }
        return (BlockActive);
    }

      
       public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            healthbar.SetHealth(currentHealth);
             
        }


    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //When Health is activated 
        if (other.CompareTag("HealthPotion"))
        {
            hasHealthPotion = true;
            if (hasHealthPotion == true)
            {
                currentHealth = +200;
                HealthBarScript.slider.value = +200;
                
                
                Debug.Log("You have gain Health");
                StartCoroutine(HealthPotionRoutine());
            }

            Destroy(other.gameObject);


        }
        //Key Action
        if (other.CompareTag("Key"))
        {
            hasKey = true;
            if (hasKey == true)
            {
                Mission3.gameObject.SetActive(true);
                Mission2.gameObject.SetActive(false);
                MainGate.SetActive(false);
                MainGateIns.SetActive(false);

            }
        }
        //When Power activiated
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            if(hasPowerup==true)
            {
                Debug.Log("Debug(You have Strength powerup)");
                StartCoroutine(StrengthRoutine());

            }
            Destroy(other.gameObject);
        }
        //When Princess activiated 
        if (other.CompareTag("Princess"))
        {
            Debug.Log("Game Completed");
            Done.gameObject.SetActive(true);
            Mission3.gameObject.SetActive(false);

        }

    }
    //When player collects Helth Potion
    IEnumerator HealthPotionRoutine()
    {
        yield return new WaitForSeconds(2);
        hasHealthPotion = false;
    }

    //When Player collect a Power up
    IEnumerator StrengthRoutine()
    {
         Strength = 100;
        PoweupUI.gameObject.SetActive(true);
        PowerupIndicator.gameObject.SetActive(true);
        powerupEmission.Play(true);
        yield return new WaitForSeconds(30);
        hasPowerup = false;
        Strength = 20;
        PoweupUI.gameObject.SetActive(false);
        powerupEmission.Play(false);
        PowerupIndicator.gameObject.SetActive(false);
        Debug.Log("Debug(Deavtiavted)");
    }



    //When player gets hit
    public IEnumerator GotHit()
    {
        
       PlayerAnim.SetBool("Hit",true);
       PlayerAnim.Play("GetHit");
       yield return new WaitForSeconds(2);
       PlayerAnim.SetBool("Hit", false);


    }





    //When Player Dies
    void onDeath()
    {
        if(currentHealth == 0)
        {
            Debug.Log("Game Over");
            GameOverGui.gameObject.SetActive(true);
            RestartGui.gameObject.SetActive(true);
            
            GameOver = true;
            PlayerAnim.SetBool("Death_b", true);
            PlayerAnim.SetInteger("DeathType_int", 1);
           

        }
    }


    
    








}
