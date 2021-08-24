using System.Collections.Generic;
using UnityEngine;

public class RoadSegmentCreator
{
    private List<DefaultPlatform> _platformSetting;
    private RoadSegmentKeeper _platformsBlockKeeper;
    private PlatformTypeIdentifier _platformTypeIdentifier;

    public RoadSegmentCreator(List<DefaultPlatform> platformSettings)
    {
        _platformSetting = platformSettings;
        _platformTypeIdentifier = new PlatformTypeIdentifier(_platformSetting);
    }

    public void DefineRoadSegment(RoadSegmentKeeper platformsBlockScript)
    {
        _platformsBlockKeeper = platformsBlockScript;

        _platformTypeIdentifier.SetRandomPlatforms(_platformsBlockKeeper);
    }
}
public class PlatformTypeIdentifier
{
    private RoadSegmentKeeper _platformsBlockKeeper;
    private List<DefaultPlatform> _platformSetting;
    private List<DefaultPlatform> _platformSettingWithPositiveLayerChanging;
    private List<DefaultPlatform> _platformSettingWithNegativeLayerChanging;

    public PlatformTypeIdentifier(List<DefaultPlatform> platformSetting)
    {
        _platformSetting = platformSetting;
    }

    public void SetRandomPlatforms(RoadSegmentKeeper platformsBlockKeeper)
    {
        int countPositivePlatforms = 0;
        int countNegativePlatforms = 0;
        _platformSettingWithPositiveLayerChanging = new List<DefaultPlatform>();
        _platformSettingWithNegativeLayerChanging = new List<DefaultPlatform>();
        _platformsBlockKeeper = platformsBlockKeeper;

        for (int i = 0; i < 3; i++)
        {
            _platformsBlockKeeper.PlatformKeepers[i].Init(_platformSetting[Random.Range(0, 
                                                                           _platformSetting.Count)]);
            if(GetLayerChanging(i) >= 0)
            {
                countPositivePlatforms += 1;
            }
            else
            {
                countNegativePlatforms += 1;
            }
        }
        for (int i = 0; i < _platformSetting.Count; i++)
        {
            if (_platformSetting[i].ChangingScale >= 0)
            {
                _platformSettingWithPositiveLayerChanging.Add(_platformSetting[i]);
            }

            else if (_platformSetting[i].ChangingScale < 0)
            {
                _platformSettingWithNegativeLayerChanging.Add(_platformSetting[i]);
            }
        }
        if (countNegativePlatforms < 1)
        {
            _platformsBlockKeeper.PlatformKeepers[
                Random.Range(0, 3)].Init(_platformSettingWithNegativeLayerChanging[Random.Range(0,
                _platformSettingWithNegativeLayerChanging.Count)]);
        }
        if (countPositivePlatforms < 1)
        {
            _platformsBlockKeeper.PlatformKeepers[
                Random.Range(0, 3)].Init(_platformSettingWithPositiveLayerChanging[Random.Range(0, 
                _platformSettingWithPositiveLayerChanging.Count)]);
        }
    }
    private int GetLayerChanging(int numberPlatform)
    {
        return _platformsBlockKeeper.PlatformKeepers[numberPlatform].PlatformType.ChangingScale;
    }
}