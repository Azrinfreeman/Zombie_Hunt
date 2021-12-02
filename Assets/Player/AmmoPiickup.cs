using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class AmmoPiickup : MonoBehaviour
{
   
    public UIController ui;
    public PlayerController player;
    public AudioSource audioPick;

    public bool collected;

    public bool mm9Box;
    public bool mm7Box;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !collected && mm7Box)
        {
            player.allGuns[2].GetAmmo7();
           
            ui.detailCollected.text += "+30 "+"AK ammo collected\n";
            ui.uiAnim.SetTrigger("isFading");
            audioPick.PlayOneShot(audioPick.clip);
            Destroy(gameObject);
            collected = true;
        }
        else if (other.tag == "Player" && !collected && mm9Box)
        {
            player.allGuns[1].GetAmmo9();
           
            ui.detailCollected.text += "+15 " + "Pistol ammo collected\n";
            ui.uiAnim.SetTrigger("isFading");
            audioPick.PlayOneShot(audioPick.clip);
            Destroy(gameObject);
            collected = true;
        }
    }
    void Start()
    {
        ui = GameObject.Find("Canvas").GetComponent<UIController>();
        player = GameObject.Find("player").GetComponent<PlayerController>();
        audioPick = GameObject.Find("audio_pickup").GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pickupAmmo()
    {
        if (mm7Box)
        {

            player.allGuns[2].GetAmmo7();
            
            ui.detailCollected.text += "+30 " + "AK ammo collected\n";
           
            ui.uiAnim.SetTrigger("isFading");
            audioPick.PlayOneShot(audioPick.clip) ;
            Debug.Log("audio played");
            Destroy(gameObject);
            collected = true;
            
        }
        else if (mm9Box)
        {

            player.allGuns[1].GetAmmo9();
            
            ui.detailCollected.text += "+15 " + "Pistol ammo collected\n";
           
            ui.uiAnim.SetTrigger("isFading");
            audioPick.PlayOneShot(audioPick.clip);
            Debug.Log("audio played");
            Destroy(gameObject);
            collected = true;
           
        }
       
        
    }

   

    
}
