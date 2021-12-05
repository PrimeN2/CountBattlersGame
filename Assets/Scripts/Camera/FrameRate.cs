using UnityEngine;

public class FrameRate : MonoBehaviour
{
    [SerializeField] private int _frameRate = 60;

    private void Update()
    {
        QualitySettings.vSyncCount = 0;

        if (_frameRate != Application.targetFrameRate)
            Application.targetFrameRate = _frameRate;
    }
}

