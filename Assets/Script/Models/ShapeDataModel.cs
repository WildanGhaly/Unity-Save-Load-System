using System;

[Serializable]
public class ShapeDataModel
{
    public string type; // "square" or "circle"
    public float[] position;
    public float[] scale;
    public float[] color;
    public float[] rotation;
}
