using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
{
    public Slider loadingSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void startButton()
    {
        StartCoroutine(loadScenePlay());
    }

    IEnumerator loadScenePlay()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("level1");

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
