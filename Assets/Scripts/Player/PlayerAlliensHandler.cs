using System.Collections.Generic;
using UnityEngine;

public class PlayerAlliensHandler : MonoBehaviour
{
    private List<CharacterKeeper> _characters;

    private void Awake()
    {
        _characters = new List<CharacterKeeper>();
    }

    public void AddCharacter(CharacterKeeper character)
    {
        _characters.Add(character);
    }

    public void SetDestination(Vector3 destination)
    {
        foreach(var character in _characters)
        {
            character.SetDestination(destination);
        }
    }

    public Vector3 GetPositionForCharacters()
    {
        return transform.position;
    }
}
