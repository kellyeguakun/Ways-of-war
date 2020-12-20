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
    public bool GotHit;
    



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
        //Enemy AI follow
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
        }



        EnemyAnim.Play("Run");






        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
        //Debugging Tool
        if (Input.GetKeyDown(KeyCode.L))
        {

            TakeDamage(20);
            StartCoroutine(onHit());
        }

        Death();
    }

    //Enemy Take damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(onHit());
        EnemyAnim.Play("GetHit");
        EnemyAnim.Play("Idle");
        healthbar.SetHealth(currentHealth);
    }
    
    //When emeny dies
    void Death()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    //When Enemy is hitted
    public IEnumerator onHit()
    {
        EnemyAnim.Play("GetHit");
         enemyRb.isKinematic=true;
         yield return new WaitForSeconds(1);
        enemyRb.isKinematic = false;
        EnemyAnim.Play("Run");
        Debug.Log("They Froze");

    }

    //Area where enemy spots Player
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }


}
