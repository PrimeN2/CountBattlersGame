using System.Collections.Generic;
using UnityEngine;

public class PlatformsBlockCreator
{
    private List<DefaultPlatform> _platformSetting;
    private PlatformsBlockKeeper _platformsBlockKeeper;
    private LayerChangingHandler _layerChangingHandler;

    public PlatformsBlockCreator(List<DefaultPlatform> platformSettings)
    {
        _platformSetting = platformSettings;
        _layerChangingHandler = new LayerChangingHandler(_platformSetting);
    }

    public void DefinePlatformsBlock(PlatformsBlockKeeper platformsBlockScript)
    {
        _platformsBlockKeeper = platformsBlockScript;

        _layerChangingHandler.SetRandomPlatforms(_platformsBlockKeeper);
    }
}
public class LayerChangingHandler
{
    private PlatformsBlockKeeper _platformsBlockKeeper;
    private List<DefaultPlatform> _platformSetting;
    private List<DefaultPlatform> _platformSettingWithPositivLayerChanging;

    public LayerChangingHandler(List<DefaultPlatform> platformSetting)
    {
        _platformSetting = platformSetting;
    }

    public void SetRandomPlatforms(PlatformsBlockKeeper platformsBlockKeeper)
    {
        int countPositivPlatforms = 0;
        _platformSettingWithPositivLayerChanging = new List<DefaultPlatform>();
        _platformsBlockKeeper = platformsBlockKeeper;

        for (int i = 0; i < 3; i++)
        {
            _platformsBlockKeeper.PlatformKeepers[i].Init(_platformSetting[Random.Range(0, 
                                                                           _platformSetting.Count)]);
            if(GetLayerChanging(i) >= 0)
            {
                countPositivPlatforms += 1;
            }
        }
        for(int i = 0; i < _platformSetting.Count; i++)
        {
            if (_platformSetting[i].ChangingLayer >= 0)
            {
                _platformSettingWithPositivLayerChanging.Add(_platformSetting[i]);
            }
        }
        if(countPositivPlatforms < 1)
        {
            _platformsBlockKeeper.PlatformKeepers[
                Random.Range(0, 3)].Init(_platformSettingWithPositivLayerChanging[Random.Range(0, 
                _platformSettingWithPositivLayerChanging.Count)]);
        }
    }
    private int GetLayerChanging(int numberPlatform)
    {
        return _platformsBlockKeeper.PlatformKeepers[numberPlatform].PlatformType.ChangingLayer;
    }
}