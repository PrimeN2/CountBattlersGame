using UnityEngine;

public class FrameRate : MonoBehaviour
{
    [SerializeField] private int _frameRate = 60;

    void Start()
    {
        QualitySettings.vSyncCount = 0;

        if (_frameRate != Application.targetFrameRate)
            Application.targetFrameRate = _frameRate;
    }
}


