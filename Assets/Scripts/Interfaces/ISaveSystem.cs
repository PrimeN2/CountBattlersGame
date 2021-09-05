using UnityEngine;

public interface ISaveSystem
{
    void Save(PlayerData playerData);

    PlayerData Load();

    void DeleteSaves();
}
