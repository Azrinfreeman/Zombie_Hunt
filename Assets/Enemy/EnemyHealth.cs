using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth = 10;

    public EnemyController enemyHit;

    // Start is called before the first frame update
    void Start()
    {
        enemyHit = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damageEnemy(int damageTaken)
    {
        currentHealth -=damageTaken;
        if (currentHealth >0)
        {
            if (!enemyHit.hitAudio.isPlaying)
            {

                enemyHit.hitAudio.Play();

            }
        }
        else if (currentHealth <=0)
        {
            StartCoroutine(death());
        }
    }


    private IEnumerator death()
    {
        yield return new WaitForSeconds(10f);

        Destroy(gameObject);
    }
}
