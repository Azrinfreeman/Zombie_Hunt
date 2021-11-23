using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPiickup : MonoBehaviour
{
    public PlayerController player;
    public bool collected;

    public bool mm9Box;
    public bool mm7Box;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !collected && mm7Box)
        {
            player.activeGun.GetAmmo7();
            Destroy(gameObject);
            collected = true;
        }
        if (other.tag == "Player" && !collected && mm9Box)
        {
            player.activeGun.GetAmmo9();
            Destroy(gameObject);
            collected = true;
        }
    }
    void Start()
    {
        player = GameObject.Find("player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
