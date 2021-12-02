using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Text currentAmmoText;
    public Text maxAmmoText;
    
    public TextMeshProUGUI detailCollected;
    public Animator uiAnim;
    void Start()
    {
       
        detailCollected = GameObject.Find("collectedDetails").GetComponent<TextMeshProUGUI>();
        uiAnim = GameObject.Find("collectedDetails").GetComponent<Animator>();
        uiAnim.SetTrigger("isFading");
    }

    // Update is called once per frame
    void Update()
    {
        if (uiAnim.GetCurrentAnimatorStateInfo(0).IsName("UIDetails"))
        {
            detailCollected.text = "";

        }
    }

    
  
}
