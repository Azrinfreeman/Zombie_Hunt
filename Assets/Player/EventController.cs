using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventController : MonoBehaviour
{
    public static EventController instance;
    [SerializeField]
    private TextMeshProUGUI itemDescText;
    [SerializeField]
    private RectTransform itemPromptRoot;

    public bool pressable;
    public bool pressable2;
    [SerializeField]
    public bool boss1Defeated;
    public bool boss2Defeated;
    public bool boss3Defeated;

    public AudioSource boxDestroyed;
    public AudioSource carDoorSound;
    public GameObject NotePanel;
    public GameObject hordeZombies;


   
    public bool notes = false;
    // Start is called before the first frame update

  
    void Start()
    {
        carDoorSound = GameObject.Find("car_door").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pressable) {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (boss1Defeated)
                {

                    StartCoroutine(loadScenePlay());

                } else if (boss2Defeated)
                {
                    StartCoroutine(loadScenePlay2());

                } else if (boss3Defeated)
                {
                    StartCoroutine(loadVictory());    
                }
            }
        }
        if (pressable2) {
            if (Input.GetKeyDown(KeyCode.E))
            {
                NotePanel.SetActive(true);
                
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !notes &&boss1Defeated || boss2Defeated || boss3Defeated)
        {
            itemPromptRoot.gameObject.SetActive(true);
            itemDescText.text = "Escape with the car";
            if (boss3Defeated)
            {

                itemDescText.text = "Destroy the box and escape";
            }
            pressable = true;
            
            
        }

        if (other.tag == "Player" && notes)
        {
            itemPromptRoot.gameObject.SetActive(true);
            itemDescText.text = "Read notes";
            UIController.instance.objDesc.text = "-Defeat the all infected";
            pressable2 = true;
            
        }
    }
 
    IEnumerator loadScenePlay()
    {
        carDoorSound.Play();
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("cutscene1_1");

    }
    IEnumerator loadScenePlay2()
    {
        carDoorSound.Play();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("cutscene1_2");

    }

    IEnumerator loadVictory()
    {
        boxDestroyed.Play();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("cutscene_victory");

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && pressable2)
        {
            hordeZombies.SetActive(true);
           
        }
        if (other.tag == "Player")
        {
            itemPromptRoot.gameObject.SetActive(false);
            NotePanel.SetActive(false);
            pressable = false;
            pressable2 = false;
        }

      
    }
}
