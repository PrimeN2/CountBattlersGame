using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinarySaveSystem : ISaveSystem
{
    private readonly string _filePath;

    public BinarySaveSystem()
    {
        _filePath = Application.persistentDataPath + "/Save.dat";
    }

    public void Save(PlayerData playerData)
    {
        using (FileStream file = File.Create(_filePath))
        {
            new BinaryFormatter().Serialize(file, playerData);
        }
    }

    public PlayerData Load()
    {
        PlayerData saveData;
        try
        {
            using (FileStream file = File.Open(_filePath, FileMode.Open))
            {
                object loadedData = new BinaryFormatter().Deserialize(file);
                saveData = (PlayerData)loadedData;
            }
        }
        catch
        {
            saveData = new PlayerData(0, 0);
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
