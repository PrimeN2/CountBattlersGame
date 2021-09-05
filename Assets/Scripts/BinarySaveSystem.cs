using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinarySaveSystem : ISaveSystem
{
    private readonly string _filePath;
    private const string _isFirstLaunchKey = "IsFirstLaunch";

    public BinarySaveSystem()
    {
        _filePath = Application.persistentDataPath + "/Save.dat";
        if (!PlayerPrefs.HasKey(_isFirstLaunchKey))
        {
            PlayerPrefs.SetInt(_isFirstLaunchKey, 1);
            PlayerPrefs.Save();
        }
    }

    public void Save(PlayerData playerData)
    {
        if (PlayerPrefs.HasKey(_isFirstLaunchKey))
        {
            if(PlayerPrefs.GetInt(_isFirstLaunchKey) == 1)
            {
                PlayerPrefs.SetInt(_isFirstLaunchKey, 0);
                PlayerPrefs.Save();
            }

        }
        using (FileStream file = File.Create(_filePath))
        {
            new BinaryFormatter().Serialize(file, playerData);
        }
    }

    public PlayerData Load()
    {
        if (PlayerPrefs.HasKey(_isFirstLaunchKey))
        {
            if (PlayerPrefs.GetInt(_isFirstLaunchKey) == 1)
                return new PlayerData(0, 0);
        }

        PlayerData saveData;
        using (FileStream file = File.Open(_filePath, FileMode.Open))
        {
            object loadedData = new BinaryFormatter().Deserialize(file);
            saveData = (PlayerData)loadedData;
        }

        return saveData;
    }

    public void DeleteSaves()
    {
        using (FileStream file = File.Open(_filePath, FileMode.Open))
        {
            new BinaryFormatter().Serialize(file, new PlayerData(0, 0));
        }
    }
}
