using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class pickupItem : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private TextMeshProUGUI itemDescText;
    [SerializeField]
    private RectTransform itemPromptRoot;


    private Item itemBeingPickup;
    public AmmoPiickup Pick;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SelectItemBeingPickupUpFromRay();

        if (HasItemTargetted())
        {
            itemPromptRoot.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Pick.pickupAmmo();
            }
            else
            {

            }
        }
        else
        {
            itemPromptRoot.gameObject.SetActive(false);

        }
    }

    private bool HasItemTargetted()
    {
        return itemBeingPickup != null;
    }

    private void SelectItemBeingPickupUpFromRay()
    {
        Ray ray = camera.ViewportPointToRay(Vector3.one / 2f);
        Debug.DrawRay(ray.origin, ray.direction * 2f, Color.red);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, 1.3f, layerMask))
        {
            var HitItem = hitInfo.collider.GetComponent<Item>();

            Pick = hitInfo.collider.GetComponent<AmmoPiickup>();
            if(HitItem == null)
            {
                itemBeingPickup = null;
            }
            else if(HitItem != null && HitItem != itemBeingPickup)
            {
                itemBeingPickup = HitItem;
                if (Pick.mm9Box)
                {
                    itemDescText.text = "9mm ammo for glock";
                }else if (Pick.mm7Box)
                {

                    itemDescText.text = "7mm ammo for AK47";
                }
            }
        }else
        {
            itemBeingPickup = null;
        }
    }
}
