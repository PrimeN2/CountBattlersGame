using UnityEngine;

public abstract class DefaultPlatform : ScriptableObject
{
    public abstract void Accept(IPlatformVisiter platformVisiter, Transform transform);

    public Material Material { get => _material; protected set => _material = value; }
    [SerializeField] protected Material _material;
    public int ChangingScale { get => _changingLayer; protected set => _changingLayer = value; }
    [SerializeField] protected int _changingLayer;
}
