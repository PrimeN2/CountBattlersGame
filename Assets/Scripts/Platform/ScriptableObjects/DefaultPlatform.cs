using UnityEngine;

public abstract class DefaultPlatform : ScriptableObject
{
    public abstract void Accept(IPlatformVisitor platformVisiter, Transform transform);

    public Material Material { get => _material; }
    [SerializeField] protected Material _material;

    public int ChangingScale { get => _changingLayer; }
    [SerializeField] protected int _changingLayer;
}
