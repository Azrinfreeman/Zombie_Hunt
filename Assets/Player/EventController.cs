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
    [SerializeField]
    public bool boss1Defeated;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pressable) {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(loadScenePlay());
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && boss1Defeated)
        {
            itemPromptRoot.gameObject.SetActive(true);
            itemDescText.text = "Escape with the car";

            pressable = true;
            
            
        }
    }
    IEnumerator loadScenePlay()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("level2");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            //loadingSlider.value = asyncLoad.progress;
            yield return null;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            itemPromptRoot.gameObject.SetActive(false);
            pressable = false;
        }
    }
}
