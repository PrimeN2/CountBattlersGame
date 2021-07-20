using UnityEngine.SceneManagement;
using UnityEngine;

public class ManageUI : MonoBehaviour
{
    private Scene _activeScene;

    public void RestartScene()
    { 
        _activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(_activeScene.name);
    }
}
