using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _percentage;
    [SerializeField] private TextMeshProUGUI _label;

    private AsyncOperation _loadingSceneOperation;

    public void Init()
    {
        DontDestroyOnLoad(transform.parent);
    }

    public void CloseLoadingScreen(AsyncOperation operation)
    {
        _loadingSceneOperation = operation;

        StartCoroutine(CloseLoadingScreen());
        StartCoroutine(CountPercents());
    }

    private IEnumerator CloseLoadingScreen()
    {
        _label.gameObject.SetActive(false);
        _percentage.gameObject.SetActive(false);
        Image screen = GetComponent<Image>();

        for (int i = 0; i < 10; i++)
        {
            screen.color -= new Color(0, 0, 0, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }

        SceneController.Instance.IsAnyOperationsGoing = false;
        Destroy(transform.parent.gameObject);
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
}
