using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;


public class SystemSceneManager : MonoBehaviour
{
    [SerializeField] Canvas loadingCanva;
    [SerializeField] float fillSpeed = 0.5f;
    [SerializeField] Image loadingBar;
    [SerializeField] GameObject loadinCamera;

    float targetProgress;
    bool isLoading;

    private static SystemSceneManager instance;
    private string sceneManagerName;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            
            loadingCanva.gameObject.SetActive(false);
            sceneManagerName = gameObject.scene.name;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        if (isLoading)
        {
            float currentFillAmount = loadingBar.fillAmount;
            float progressDifference = Mathf.Abs(currentFillAmount - targetProgress);

            float dynamicFillSpeed = progressDifference * fillSpeed;

            loadingBar.fillAmount = Mathf.Lerp(currentFillAmount, targetProgress, Time.deltaTime * dynamicFillSpeed);

        }
    }
    public async Task LoadScene(int i, float duration = 1, float waitTime = 0)
    {
        loadingBar.fillAmount = 0;
        targetProgress = 1;

        LoadingProgress progress = new LoadingProgress();

        progress.OnProgress += taget => targetProgress = Mathf.Max(targetProgress, targetProgress);

        EnableLoadingCanvas();

        AsyncOperation ao = SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
        while (!ao.isDone)
        {
            await Task.Delay(100);
        }

        Scene nextScene = SceneManager.GetSceneAt(i);
        SceneManager.SetActiveScene(nextScene);

        EnableLoadingCanvas(false);
    }

    public async Task UnloadScene()
    {
        var scenes = new List<string>();
        string activeScene = SceneManager.GetActiveScene().name;

        int scenecount = SceneManager.sceneCount;

        for (int i = scenecount - 1; i > 0; i--)
        {
            Scene sceneAt = SceneManager.GetSceneAt(i);

            if (sceneAt.isLoaded)
            {
                string sceneName = sceneAt.name;
                if(!sceneName.Equals(activeScene) && !(sceneName == sceneManagerName))
                {
                    scenes.Add(sceneName);
                }
            }
        }
        var operationGroup = new AsyncOperationGroup(scenes.Count);

        foreach (string scene in scenes)
        {
            AsyncOperation operacion =  SceneManager.UnloadSceneAsync(scene);
            if(operacion != null)
            {
                operationGroup.operations.Add(operacion);
            }
        }

        // Espera a que terminen todas las descargas del grupo operationGroup(Normalmente sera solo 1)
        while (!operationGroup.IsDone)
        {
            await Task.Delay(100);
        }

    }

    void EnableLoadingCanvas(bool enable = true)
    {
        isLoading = enable;
        loadingCanva.gameObject.SetActive(enable);
        loadinCamera.SetActive(enable);
    }


    public readonly struct AsyncOperationGroup
    {
        public readonly List<AsyncOperation> operations;

        public float Progress => operations.Count == 0 ? 0 : operations.Average(o => o.progress);
        public bool IsDone => operations.All(o => o.isDone);

        public AsyncOperationGroup(int initialCapacity)
        {
            operations = new List<AsyncOperation>(initialCapacity);
        }
    }

}
public class LoadingProgress : IProgress<float>
{
    public event Action<float> OnProgress;

    const float ratio = 1;

    public void Report(float value)
    {
        OnProgress?.Invoke(value/ratio);
    }
}
