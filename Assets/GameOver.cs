using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameOver : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject retryBtn;
    public GameObject quitBtn;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        videoPlayer.loopPointReached += EndReached;

    }

    public void Restart1()
    {
        StartCoroutine(loadScenePlay1());
    }

    IEnumerator loadScenePlay1()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("level1");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            //  loadingSlider.value = asyncLoad.progress;
            yield return null;
        }
    }
    public void Restart2()
    {
        StartCoroutine(loadScenePlay2());
    }

    IEnumerator loadScenePlay2()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("level2");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            //  loadingSlider.value = asyncLoad.progress;
            yield return null;
        }
    }

    public void Restart3()
    {
        StartCoroutine(loadScenePlay3());
    }

    IEnumerator loadScenePlay3()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("level3");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            //  loadingSlider.value = asyncLoad.progress;
            yield return null;
        }
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        retryBtn.SetActive(true);
        quitBtn.SetActive(true);
    }

    public void ReturnMain()
    {
        SceneManager.LoadScene("Main");
    }
}
