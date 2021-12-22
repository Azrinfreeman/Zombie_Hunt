using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class cutscene1 : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject nextBtn;
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

    public void Next()
    {
        StartCoroutine(loadScenePlay1());
    }

    IEnumerator loadScenePlay1()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("level2");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            //  loadingSlider.value = asyncLoad.progress;
            yield return null;
        }
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        nextBtn.SetActive(true);
    }
}
