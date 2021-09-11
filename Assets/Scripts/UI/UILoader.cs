using System;
using UnityEngine;

public class UILoader : MonoBehaviour
{
    public Action OnMenuLoaded;
    public Action OnMenuHided;

    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _pauseMenuButton;
    [SerializeField] private GameObject _infoPanel;

    private void Start()
    {
        LoadMenu();
    }

    public void LoadMenu()
    {
        OnMenuLoaded?.Invoke();
        _mainMenuPanel.SetActive(true);
        _pauseMenuButton.SetActive(false);
        _infoPanel.SetActive(false);
    }

    public void HideMenu()
    {
        OnMenuHided?.Invoke();
        _mainMenuPanel.SetActive(false);
        _pauseMenuButton.SetActive(true);
        _infoPanel.SetActive(true);
    }
}
