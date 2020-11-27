using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody enemyRb;
    GameObject Player;
    public float speed;
    public int maxHealth;
    public int currentHealth;
    public EnemyHealth healthbar;
    private Animator EnemyAnim;
    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;



    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        Player = GameObject.Find("Player");
        EnemyAnim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
         if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);
        }
        
        
        //Vector3 lookDirection = (Player.transform.position - transform.position).normalized;
        //enemyRb.AddForce(lookDirection * speed);
        EnemyAnim.Play("Run");

        
        
        
        
        
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

    void Death()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator onHit()
    { 

         enemyRb.isKinematic=true;
         yield return new WaitForSeconds(1);
        enemyRb.isKinematic = false;
        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }


}
