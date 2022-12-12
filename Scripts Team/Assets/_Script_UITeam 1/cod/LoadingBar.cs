using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingBar : MonoBehaviour
{
    private bool loadScene = false;
    public string LoadingSceneName;
    public Text loadingText;
    public Slider SliderBar;

    void Start()
    {
    SliderBar.gameObject.SetActive(false);
    }
    void Update()
    {
          loadScene = true;

      
        SliderBar.gameObject.SetActive(true);
       
        loadingText.text = "Loading...";
       
        StartCoroutine(LoadNewScene(LoadingSceneName));

    

    }


    IEnumerator LoadNewScene(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        while (!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            SliderBar.value = progress;
            loadingText.text = progress * 100f + "%";
            yield return null;

        }
    }
}
