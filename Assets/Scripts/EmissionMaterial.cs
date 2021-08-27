using UnityEngine;

[System.Serializable]
public class EmissionMaterial
{
    public Material Material;
    public Color EmissionColor;

    public EmissionMaterial(Material material, Color emissionColor)
    {
        Material = material;
        EmissionColor = emissionColor;
    }

    public override bool Equals(object obj)
    {
        if ((obj == null) || !GetType().Equals(obj.GetType()))
            return false;
        return EmissionColor == ((EmissionMaterial)obj).EmissionColor;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
