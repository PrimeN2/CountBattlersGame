using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SingletonPersistent<SceneController>
{
    public bool IsAnyOperationsGoing = false;

    [SerializeField] private LoadingScreen _loadingScreen;

    private AsyncOperation _loadingSceneOperation;

    private void OnEnable()
    {
        _loadingScreen.Init();

        _loadingSceneOperation = SceneManager.LoadSceneAsync((int)Scenes.Main);
        _loadingSceneOperation.completed += _loadingScreen.CloseLoadingScreen;
        IsAnyOperationsGoing = true;
    }

    public void StartDelayedReloading()
    {
        if (IsAnyOperationsGoing)
            return;
        _loadingSceneOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        
        _loadingSceneOperation.allowSceneActivation = false;
        IsAnyOperationsGoing = true;
    }

    public void ReloadDelayedScene()
    {
        _loadingSceneOperation.allowSceneActivation = true;
        IsAnyOperationsGoing = false;
    }
}

public enum Scenes
{
    BootScene = 0,
    Main = 1
}

