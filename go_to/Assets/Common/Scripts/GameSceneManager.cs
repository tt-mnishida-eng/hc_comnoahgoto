using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public bool IsLoading { get; set; } = false;
    public string CurrentMainSceneName { get; private set; } = default;

    public Scene CurrentMainScene
    {
        get { return SceneManager.GetSceneByName(CurrentMainSceneName); }
    }

    List<string> subScenes = new List<string>();

    public void Initialize()
    {
        CurrentMainSceneName = string.Empty;
    }

    public void LoadAsyncScene(string sceneName, Action onStart = null, Action<Scene> onComplete = null)
    {
        IsLoading = true;
        CurrentMainSceneName = sceneName;
        StartCoroutine(DoSceneLoading(sceneName, onStart, (Scene scene) =>
        {
            IsLoading = false;
            SwitchActiveScene(scene);
            if (onComplete != null) onComplete(scene);
        }));
    }

    public void LoadAsyncSubScene(string sceneName, Action onStart = null, Action<Scene> onComplete = null)
    {
        IsLoading = true;
        CurrentMainSceneName = sceneName;
        StartCoroutine(DoSceneLoading(sceneName, onStart, (Scene scene) =>
        {
            IsLoading = false;
            subScenes.Add(sceneName);
            if (onComplete != null) onComplete(scene);
        }));
    }

    IEnumerator DoSceneLoading(string sceneName, Action onStart = null, Action<Scene> onComplete = null)
    {
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        if (onStart != null) onStart();
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        var scene = SceneManager.GetSceneByName(sceneName);

        if (onComplete != null) onComplete(scene);
        yield break;
    }

    public void UnloadAsyncScene(string sceneName, Action onStart = null, Action onComplete = null)
    {
        StartCoroutine(DoSceneUnLoading(sceneName, onStart, () =>
        {
            CurrentMainSceneName = string.Empty;
            if (onComplete != null) onComplete();
        }));
    }

    public void UnloadAsyncSubScene(string sceneName, Action onStart = null, Action onComplete = null)
    {
        StartCoroutine(DoSceneUnLoading(sceneName, onStart, () =>
        {
            subScenes.Remove(sceneName);
            if (onComplete != null) onComplete();
        }));
    }

    public void UnloadAsyncAllSubScenes(Action onComplete = null)
    {
        if (subScenes.Count == 0)
        {
            onComplete();
            return;
        }

        StartCoroutine(DoSceneUnLoading(subScenes[0], null, () =>
        {
            subScenes.Remove(subScenes[0]);
            if (subScenes.Count > 0)
            {
                UnloadAsyncAllSubScenes(onComplete);
            }
            else
            {
                if (onComplete != null) onComplete();
            }
        }));
    }

    public void SwitchActiveScene(Scene scene)
    {
        SceneManager.SetActiveScene(scene);
    }

    IEnumerator DoSceneUnLoading(string sceneName, Action onStart = null, Action onComplete = null)
    {
        var asyncLoad = SceneManager.UnloadSceneAsync(sceneName);
        if (onStart != null) onStart();
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (onComplete != null) onComplete();
        yield break;
    }
}