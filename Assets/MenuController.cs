using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
{
    public Slider loadingSlider;
    public AudioSource clickSound;
    public GameObject helpPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void startButton()
    {
        if (!clickSound.isPlaying)
        {
            clickSound.Play();
        }
        StartCoroutine(loadScenePlay());
    }

    public void ShowHelp()
    {
        if (!clickSound.isPlaying)
        {
            clickSound.Play();
        }
        helpPanel.SetActive(true);
    }


    public void HideHelp()
    {
        if (!clickSound.isPlaying)
        {
            clickSound.Play();
        }
        helpPanel.SetActive(false);
    }
    public void QuitButton()
    {
        if (!clickSound.isPlaying)
        {
            clickSound.Play();
        }
        Application.Quit();
    }

    IEnumerator loadScenePlay()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("cutscene1");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            loadingSlider.value = asyncLoad.progress;
            yield return null;
        }
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
