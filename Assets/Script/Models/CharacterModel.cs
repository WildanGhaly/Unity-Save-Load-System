using System;
using System.Collections.Generic;

[Serializable]
public class CharacterModel
{
    public List<ShapeDataModel> shapes = new();
    public string textData;
}