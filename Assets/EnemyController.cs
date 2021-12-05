using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    public Animator anim;
    public float moveSpeed;
    public Rigidbody theRB;
    private bool chasing;
    public float distanceToChase = 50f, distanceToLose = 60f, distanceToStop = 20f;

    private Vector3 targetPoint, startPoint;

    public NavMeshAgent agent;

    public float keepChasingTime = 5f;
    private float chaseCounter;

    public EnemyHealth enemyHealth;


    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        targetPoint = PlayerController.instance.transform.position;
        targetPoint.y = transform.position.y;

        if (enemyHealth.currentHealth < 0)
        {
            enemyHealth.currentHealth = 0;
        }

        if (enemyHealth.currentHealth == 0)
        {
            Debug.Log("deadd");
            anim.SetBool("isDead", true);
        }else if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("death"))
        {
            if (!chasing)
            {
                if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
                {
                    chasing = true;
                }

                if (chaseCounter > 0)
                {
                    chaseCounter -= Time.deltaTime;
                    if (chaseCounter <= 0)
                    {

                        anim.SetBool("isWalking", true);
                        agent.destination = startPoint;
                    }
                }


            }
            else
            {
                //transform.LookAt(targetPoint);

                //theRB.velocity = transform.forward * moveSpeed;
                if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
                {
                    agent.destination = targetPoint;
                    anim.SetBool("isWalking", true);
                }
                else
                {

                    anim.SetBool("isWalking", false);
                    agent.destination = transform.position;
                    if (enemyHealth.currentHealth >=0)
                    {
                        Debug.Log("Trigger attack");
                        anim.SetTrigger("attack");
                    }
                }


                if (Vector3.Distance(transform.position, targetPoint) > distanceToLose)
                {
                    chasing = false;
                    chaseCounter = keepChasingTime;
                }
            }
        }
        else
        {
            agent.destination = transform.position;
        }
    }
}
