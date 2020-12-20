using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;


public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody BossRb;
    GameObject Player;
    public float speed;
    public int maxHealth;
    public int currentHealth;
    public EnemyHealth healthbar;
    private Animator EnemyAnim;
    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;
    public bool GotHit;
    public GameObject Castle;
    public GameObject Tower;
    



    void Start()
    {
        BossRb = GetComponent<Rigidbody>();
        Player = GameObject.Find("Player");
        EnemyAnim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
    }

    
    void Update()
    {
       //Boss AI
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            EnemyAnim.Play("Run");

        }
        


      


        //Debugging  TOOL
        if (Input.GetKeyDown(KeyCode.B))
        {

            TakeDamage(20);
            StartCoroutine(onHit());
        }






        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {

            TakeDamage(20);
            StartCoroutine(onHit());
        }

        Death();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(onHit());
        EnemyAnim.Play("GetHit");
        EnemyAnim.Play("Idle");
        healthbar.SetHealth(currentHealth);
    }
    
    //When Boss dies
    void Death()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Castle.gameObject.SetActive(false);
            Tower.gameObject.SetActive(true);
           
            

        }
    }


   //When boss is hit
    public IEnumerator onHit()
    {
        EnemyAnim.Play("GetHit");
        BossRb.isKinematic = true;
        yield return new WaitForSeconds(3);
        BossRb.isKinematic = false;
        EnemyAnim.Play("Run");
        Debug.Log("They Froze");

    }

    //Are where boss can find player
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }




}