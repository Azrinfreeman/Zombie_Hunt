using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
public class EnemyController : MonoBehaviour
{
    public int enemy; 

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

    [Space(10)]
    public Slider healthSlider;
    public TextMeshProUGUI bossName;
    public GameObject rootSlider;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();

        if (enemy == 0)
        {

            alertAudio = GameObject.Find("zombie-moan").GetComponent<AudioSource>();
            deadAudio = GameObject.Find("zombie-dying").GetComponent<AudioSource>();
            hitAudio = GameObject.Find("zombie-hit").GetComponent<AudioSource>();

        }
        else if (enemy == 1)
        {
            rootSlider = GameObject.Find("bossHealth");
            bossName = GameObject.Find("name").GetComponent<TextMeshProUGUI>();
            healthSlider = GameObject.Find("bossHealth").GetComponent<Slider>();
            alertAudio = GameObject.Find("boss1-alert").GetComponent<AudioSource>();
            deadAudio = GameObject.Find("zombie-dying").GetComponent<AudioSource>();
            hitAudio = GameObject.Find("zombie-hit").GetComponent<AudioSource>();

            rootSlider.SetActive(false);

        }
        else if (enemy == 2)
        {
            rootSlider = GameObject.Find("bossHealth");
            bossName = GameObject.Find("name").GetComponent<TextMeshProUGUI>();
            healthSlider = GameObject.Find("bossHealth").GetComponent<Slider>();
            alertAudio = GameObject.Find("werewolf-alert").GetComponent<AudioSource>();
            hitAudio = GameObject.Find("werewolf-dying").GetComponent<AudioSource>();
            deadAudio = GameObject.Find("werewolf-dying").GetComponent<AudioSource>();
            healthSlider.maxValue = 40;
            rootSlider.SetActive(false);
        }
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
                if (enemy == 1)
                {
                    EventController.instance.boss1Defeated = true;
                    rootSlider.SetActive(false);
                }
                else if (enemy == 2)
                {

                    rootSlider.SetActive(false);
                }
                else if (enemy == 3)
                {

                    rootSlider.SetActive(false);
                }
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
                if (enemy == 1)
                {
                    rootSlider.SetActive(true);
                    bossName.text = "Special Infected";
                    healthSlider.value = enemyHealth.currentHealth;
                }else if (enemy == 2)
                {

                    rootSlider.SetActive(true);
                    bossName.text = "Special Infected";
                    healthSlider.value = enemyHealth.currentHealth;
                }else if (enemy == 3)
                {

                    rootSlider.SetActive(true);
                    bossName.text = "Special Infected";
                    healthSlider.value = enemyHealth.currentHealth;
                }
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
