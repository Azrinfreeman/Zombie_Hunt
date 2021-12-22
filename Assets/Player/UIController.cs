using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    
    public TextMeshProUGUI currentAmmoText;
    public TextMeshProUGUI maxAmmoText;

    public TextMeshProUGUI objDesc;
    public int level;

    [Space(10)]
    public TextMeshProUGUI detailCollected;
    public Animator uiAnim;


    [Space(10)]
    public GameObject pausePanel;
    public AudioSource clickSound;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        clickSound = GameObject.Find("buttonSelect").GetComponent<AudioSource>();
        detailCollected = GameObject.Find("collectedDetails").GetComponent<TextMeshProUGUI>();
        uiAnim = GameObject.Find("collectedDetails").GetComponent<Animator>();
        uiAnim.SetTrigger("isFading");
        pausePanel = GameObject.Find("PausePanel");
        pausePanel.SetActive(false);
        if (level == 1)
        {

            objDesc.text = "-Take the pistol and ammos on the table before\nheading to the garage for safety";

        }else if (level == 2)
        {
            objDesc.text = "-Walk along the blocked highway to find anything useful";

        }else if (level == 3)
        {
            objDesc.text = "-Explore the residential area";

        }
    }
    public void PauseLevel()
    {
        
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void resumeLevel()
    {

        clickSound.Play();
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void restartLevel()
    {
        clickSound.Play();
        Time.timeScale = 1;
        StartCoroutine(resetLevel());
    }
    IEnumerator resetLevel()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
         //   loadingSlider.value = asyncLoad.progress;
            yield return null;
        }
    }

    public void quitGame()
    {
        clickSound.Play();
        Time.timeScale = 1;
        StartCoroutine(quitLevel());
    }
    IEnumerator quitLevel()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("main");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            //   loadingSlider.value = asyncLoad.progress;
            yield return null;
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
