using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start crow");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Circle")
        {
            Debug.Log("start touching");
            other.gameObject.GetComponent<EnemyHealth>().damageEnemy(damage);
        }
        else
        {
            Debug.Log("start me");
        }
    }
}
