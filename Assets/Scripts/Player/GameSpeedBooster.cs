using UnityEngine;

public class GameSpeedBooster : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float _speedChangeValue = 1f;
    private int _barriersPassedCount = 0;

    private void Awake()
    {
        _barriersPassedCount = 0;
    }

    private void IncreaseGameSpeed(GameObject roadsegment)
    {
        _barriersPassedCount += 1;
        if (_barriersPassedCount % 5 == 0)
            _playerMovement.TryChangeSpeed(_speedChangeValue);
    }

    private void OnEnable()
    {
        RoadSegmentKeeper.OnSegmentOverFliew += IncreaseGameSpeed;
    }
    private void OnDisable()
    {
        RoadSegmentKeeper.OnSegmentOverFliew -= IncreaseGameSpeed;
    }
}
