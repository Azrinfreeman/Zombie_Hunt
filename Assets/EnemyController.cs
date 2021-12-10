using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;
    public Animator anim;
    public float moveSpeed;
    public Rigidbody theRB;
    private bool chasing;
    public float distanceToChase = 50f, distanceToLose = 60f, distanceToStop = 17f;

    private Vector3 targetPoint, startPoint;

    public NavMeshAgent agent;

    public float keepChasingTime = 5f;
    private float chaseCounter;
    public AudioSource alertAudio;
    public AudioSource deadAudio;
    public AudioSource hitAudio;
    bool alreadyDead = false;
    public EnemyHealth enemyHealth;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        alertAudio = GameObject.Find("zombie-moan").GetComponent<AudioSource>();
        deadAudio = GameObject.Find("zombie-dying").GetComponent<AudioSource>();
        hitAudio = GameObject.Find("zombie-hit").GetComponent<AudioSource>();
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
            if (!alreadyDead)
            {

                if (!deadAudio.isPlaying)
                {

                    deadAudio.Play();
                }
                alreadyDead = true;
            }
            agent.destination = transform.position;
        }
        else if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("death"))
        {
            if (!chasing)
            {
                if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
                {
                    chasing = true;
                    alertAudio.Play();
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
                        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
                        {

                            StartCoroutine(attackingDelay(0.4f));
                            
                        }
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

    public IEnumerator attackingDelay(float wait)
    {
        BoxCollider firePoints = transform.GetChild(1).GetComponent<BoxCollider>();
        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {

            anim.SetTrigger("attack");
            firePoints.enabled = !firePoints.enabled;
            yield return new WaitForSeconds(wait);
            firePoints.enabled = !firePoints.enabled;
        }
    }
}
