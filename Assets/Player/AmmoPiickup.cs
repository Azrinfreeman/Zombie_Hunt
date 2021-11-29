using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class AmmoPiickup : MonoBehaviour
{
   
    public UIController ui;
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
            ui.detailCollected.text = "+30 "+"AK ammo collected";
            ui.uiAnim.SetTrigger("isFading");
            collected = true;
        }
        if (other.tag == "Player" && !collected && mm9Box)
        {
            player.activeGun.GetAmmo9();
            Destroy(gameObject);
            ui.detailCollected.text = "+15 " + "Pistol ammo collected";
            ui.uiAnim.SetTrigger("isFading");
            collected = true;
        }
    }
    void Start()
    {
        ui = GameObject.Find("Canvas").GetComponent<UIController>();
        player = GameObject.Find("player").GetComponent<PlayerController>();
       
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pickupAmmo()
    {
        if (mm7Box)
        {

            player.activeGun.GetAmmo7();
            Destroy(gameObject);
            ui.detailCollected.text += "+30 " + "AK ammo collected\n";
           
            ui.uiAnim.SetTrigger("isFading");
            
            collected = true;
            
        }
        else if (mm9Box)
        {

            player.activeGun.GetAmmo9();
            Destroy(gameObject);
            ui.detailCollected.text += "+15 " + "Pistol ammo collected\n";
           
            ui.uiAnim.SetTrigger("isFading");
            
            collected = true;
           
        }
       
        
    }

   

    
}
