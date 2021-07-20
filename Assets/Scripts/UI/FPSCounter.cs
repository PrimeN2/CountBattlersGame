using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public int AverageFPS { get; private set; }

    [SerializeField] private int _frameRange = 60;
    private int[] _frameBuffer;
    private int _frameBufferIndex;

    private void Update()
    {
        if (_frameBuffer == null || _frameRange != _frameBuffer.Length)
        {
            InitializeBuffer();
        }

        UpdateBuffer();
        CalculateAVGFrameRate();
    }

    private void InitializeBuffer()
    {
        if(_frameRange <= 0)
        {
            _frameRange = 1;
        }

        _frameBuffer = new int[_frameRange];
        _frameBufferIndex = 0;
    }

    private void UpdateBuffer()
    {
        _frameBuffer[_frameBufferIndex++] = (int) (1f / Time.unscaledDeltaTime);
        if(_frameBufferIndex >= _frameRange)
        {
            _frameBufferIndex = 0;
        }
    }

    private void CalculateAVGFrameRate()
    {
        int sum = 0;
        for(int i = 0; i < _frameRange; i++)
        {
            sum += _frameBuffer[i];
        }

        AverageFPS = sum / _frameRange;
    }
}
