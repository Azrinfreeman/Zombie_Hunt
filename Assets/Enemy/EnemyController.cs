using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody theRB;
    private bool chasing;
    public float distanceToChase = 10f, distanceToLose = 15f, distanceToStop = 2f;

    private Vector3 targetPoint;
    private Vector3 startPoint;

    public NavMeshAgent agent;
    public float keepChasingTime = 5f;
    private float chaseCounter;


    public Animator anim;
   
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        targetPoint = PlayerController.instance.transform.position;
        targetPoint.y = transform.position.y;
        Debug.Log(targetPoint + "target POint");

        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("death"))
        {

            if (!chasing)
            {
                anim.SetBool("isWalking", false);
                if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
                {
                    chasing = true;
                }

                if (chaseCounter >0)
                {
                    chaseCounter -= Time.deltaTime;

                    if (chaseCounter <= 0)
                    {
                        agent.destination = startPoint;
                    }
                    

                }
                
            }
            else
            {
                //transform.LookAt(targetPoint);
                //theRB.velocity = transform.forward * moveSpeed;
                anim.SetBool("isWalking", true);

                if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
                {

                    agent.destination = targetPoint;

                }
                else
                {
                    //stop moving
                    agent.destination = transform.position;
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
            anim.SetBool("death",true);
        }
    }
}
