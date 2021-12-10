using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackZombie : MonoBehaviour
{

    public BoxCollider firePoint;
    public AudioSource audioHurtng;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        firePoint = GetComponent<BoxCollider>();
        audioHurtng = GameObject.Find("PlayerHurt").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealthController>().DamagePlayer(damage);
            PlayerHealthController.instance.DamagePlayer(3);
            audioHurtng.Play();
            Debug.Log("Player got hit");

        }
    }

}
