using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    // Start is called before the first frame update
    public UIController ui;
    public PlayerController player;
    public GameObject bullet;

    public bool canAutoFire;
    public float fireRate;
    [HideInInspector]
    public float fireCounter;

    public int currentAmmo, pickupAmount;
   
   
    void Start()
    {
        ui = GameObject.Find("Canvas").GetComponent<UIController>();
        player = GameObject.Find("player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fireCounter >0)
        {
            fireCounter -= Time.deltaTime;
        }
    }

    public void GetAmmo7()
    { 
            player.allGuns[2].currentAmmo += pickupAmount;
            ui.ammoText.text = "" + player.activeGun.currentAmmo.ToString();
    }

    public void GetAmmo9()
    {

        player.allGuns[1].currentAmmo += pickupAmount;
        ui.ammoText.text = "" + player.activeGun.currentAmmo.ToString();
    }
}
