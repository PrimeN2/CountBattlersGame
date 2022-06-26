using UnityEngine;

public interface ICharactersHandler
{
    void AddCharacter(CharacterKeeper characterKeeper);

    void RemoveCharacter(CharacterKeeper characterKeeper);

    Vector3 GetPositionForSpawn();
}
