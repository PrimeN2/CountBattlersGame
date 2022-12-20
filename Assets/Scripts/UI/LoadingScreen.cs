using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject _background;
    [SerializeField] private Image _progressBar;
    [SerializeField] private TextMeshProUGUI _percentage;

    private AsyncOperation _loadingSceneOperation;

    public void Init(AsyncOperation loadingSceneOperation)
    {
        DontDestroyOnLoad(transform.parent);

        _loadingSceneOperation = loadingSceneOperation;
        _loadingSceneOperation.completed += CloseLoadingScreen;

        StartCoroutine(CountPercents());
        StartCoroutine(MoveProgressBar());
    }

    private void CloseLoadingScreen(AsyncOperation operation)
    {
        StartCoroutine(CloseLoadingScreen());
    }

    private IEnumerator CloseLoadingScreen()
    {
        _background.SetActive(false);

        Image screen = GetComponent<Image>();

        for (int i = 0; i < 5; i++)
        {
            screen.color -= new Color(0, 0, 0, 0.2f);
            yield return new WaitForSeconds(0.1f);
        }

        SceneController.Instance.IsAnyOperationsGoing = false;
        Destroy(transform.parent.gameObject);
    }

    private IEnumerator CountPercents()
    {
        while (!_loadingSceneOperation.isDone)
        {
            _percentage.text = $"{Mathf.RoundToInt(_loadingSceneOperation.progress * 100)}%";
            yield return null;
        }
    }

    private IEnumerator MoveProgressBar()
    {
        while (!_loadingSceneOperation.isDone)
        {
            _progressBar.fillAmount = _loadingSceneOperation.progress;
            yield return null;
        }
    }
}
