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

    public int currentAmmo,maxAmmo, pickupAmount;

    // max ammo = 100
    // current ammo = max ammo - minus

    // minus = 20;


    public bool crowbar;
    public bool manchete;
    public bool glock;
    public bool ak47;

    public string gunName;

    public AudioSource bulletFire;
    public AudioSource reload;
    void Start()
    {
        ui = GameObject.Find("Canvas").GetComponent<UIController>();
        player = GameObject.Find("player").GetComponent<PlayerController>();

        reload = GameObject.Find("weapon-reload").GetComponent<AudioSource>();
        if (crowbar || manchete)
        {
            bulletFire = GameObject.Find("whoosh").GetComponent<AudioSource>();
        }

        if (glock)
        {
            bulletFire = GameObject.Find("GlockFire").GetComponent<AudioSource>();
        }
        if (ak47)
        {
            bulletFire = GameObject.Find("AK47").GetComponent<AudioSource>();
        }


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
            player.allGuns[2].maxAmmo += pickupAmount;
        if (player.activeGun.ak47)
        {

            ui.maxAmmoText.text = "" + player.allGuns[2].maxAmmo.ToString();
            ui.currentAmmoText.text = "" + player.allGuns[2].currentAmmo.ToString();

        }
    }

    

    public void GetAmmo9()
    {

        player.allGuns[1].maxAmmo += pickupAmount;
        if (player.activeGun.glock)
        {
            ui.currentAmmoText.text = "" + player.allGuns[1].currentAmmo.ToString();
            ui.maxAmmoText.text = "" + player.allGuns[1].maxAmmo.ToString();

        }
    }
}
