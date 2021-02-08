using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public GameObject loadingCanvas;
    private readonly int loadingIndex = 2;
    private int targetIndex;
    public static LoadingManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

  

    public void LoadScene(int index)
    {
        ShowLoading();
        targetIndex = index;
        StartCoroutine(LoadMiddleSceneAsync(loadingIndex));
    }

    IEnumerator LoadMiddleSceneAsync(int index)
    {
        Application.backgroundLoadingPriority = ThreadPriority.Low;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        if (asyncLoad.isDone)
        {
            StartCoroutine(LoadTargetSceneAsync(targetIndex));
        }
    }



    public IEnumerator LoadTargetSceneAsync(int index)
    {
        Application.backgroundLoadingPriority = ThreadPriority.Low;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }

  
   

 

    public void ShowLoading()
    {
        loadingCanvas.SetActive(true);
    }
    public void HideLoading()
    {
        loadingCanvas.SetActive(false);
    }


}
