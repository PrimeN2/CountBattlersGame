using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SceneController : SingletonPersistent<SceneController>
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private TextMeshProUGUI _percentage;
    [SerializeField] private TextMeshProUGUI _label;

    private AsyncOperation _loadingSceneOperation;
    private bool _isAnyOperationsGoing = false;

    private void Start()
    {
        DontDestroyOnLoad(_loadingScreen.transform.parent);
        _loadingSceneOperation = SceneManager.LoadSceneAsync((int)Scenes.Main);
        _loadingSceneOperation.completed += CloseLoadingScreen;
        StartCoroutine(CountPercents());
        _isAnyOperationsGoing = true;
    }

    private void CloseLoadingScreen(AsyncOperation a)
    {
        StartCoroutine(CloseLoadingScreen());
    }

    private IEnumerator CountPercents()
    {
        while (!_loadingSceneOperation.isDone)
        {
            if (_loadingSceneOperation != null)
                _percentage.text = $"{Mathf.RoundToInt(_loadingSceneOperation.progress * 100)}%";

            yield return null;
        }
    }

    private IEnumerator CloseLoadingScreen()
    {
        _label.gameObject.SetActive(false);
        _percentage.gameObject.SetActive(false);
        Image screen = _loadingScreen.GetComponent<Image>();

        for (int i = 0; i < 10; i++)
        {
            screen.color -= new Color(0, 0, 0, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(_loadingScreen.transform.parent.gameObject);
        _isAnyOperationsGoing = false;
    }

    public void StartDelayedReloading()
    {
        if (_isAnyOperationsGoing)
            return;
        _loadingSceneOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        
        _loadingSceneOperation.allowSceneActivation = false;
        _isAnyOperationsGoing = true;
    }

    public void Reload()
    {
        _loadingSceneOperation.allowSceneActivation = true;
        _isAnyOperationsGoing = false;
    }
}
