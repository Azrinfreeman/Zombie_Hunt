using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    
    public Text currentAmmoText;
    public Text maxAmmoText;

    public TextMeshProUGUI objDesc;
    public int level;

    [Space(10)]
    public TextMeshProUGUI detailCollected;
    public Animator uiAnim;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
       
        detailCollected = GameObject.Find("collectedDetails").GetComponent<TextMeshProUGUI>();
        uiAnim = GameObject.Find("collectedDetails").GetComponent<Animator>();
        uiAnim.SetTrigger("isFading");

        if (level == 1)
        {

            objDesc.text = "-Take the pistol and ammos on the table before\nheading to the garage for safety";

        }
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
