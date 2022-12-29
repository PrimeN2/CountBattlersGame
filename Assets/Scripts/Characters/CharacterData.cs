using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public CharacterKeeper[] CharacterSkinsPrefabs { get => _characterSkinsPrefabs; }
    [SerializeField] private CharacterKeeper[] _characterSkinsPrefabs;

    public Material[] CharacterChromasMaterials { get => _characterChromasMaterials; }
    [SerializeField] private Material[] _characterChromasMaterials;
}
