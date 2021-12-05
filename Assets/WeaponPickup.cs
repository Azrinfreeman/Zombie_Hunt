using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public UIController ui;
    public AudioSource audioPick;
    public string theGun;

    private bool collected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !collected && theGun =="glock")
        {
            PlayerController.instance.AddGun(theGun);
            ui.detailCollected.text += "Glock Pistol\n";
            ui.uiAnim.SetTrigger("isFading");
            audioPick.PlayOneShot(audioPick.clip);
            Destroy(gameObject);

            collected = true;
        }else if (other.tag == "Player" && !collected && theGun == "ak47")
        {
            PlayerController.instance.AddGun(theGun);
            ui.detailCollected.text += "AK47";
            ui.uiAnim.SetTrigger("isFading");
            audioPick.PlayOneShot(audioPick.clip);
            Destroy(gameObject);

            collected = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickupWeapon()
    {
        if (theGun == "glock")
        {
            PlayerController.instance.AddGun(theGun);
            ui.detailCollected.text += "Glock Pistol";
            ui.uiAnim.SetTrigger("isFading");
            audioPick.PlayOneShot(audioPick.clip);
            Destroy(gameObject);

            collected = true;
        }else if (theGun == "ak47")
        {
            PlayerController.instance.AddGun(theGun);
            ui.detailCollected.text += "AK47";
            ui.uiAnim.SetTrigger("isFading");
            audioPick.PlayOneShot(audioPick.clip);
            Destroy(gameObject);

            collected = true;
        }
        

    }
}
