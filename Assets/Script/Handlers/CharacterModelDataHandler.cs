using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class CharacterModelDataHandler
{
    public CharacterModel CollectCharacterData(Transform characterModelParent, TMP_InputField textData)
    {
        var shapes = characterModelParent.transform.Cast<Transform>()
                    .Where(child => child.gameObject.activeInHierarchy)
                    .Select(child => new ShapeDataModel
                    {
                        type = child.name.Contains("Square") ? "square" : "circle",
                        position = new float[] { child.localPosition.x, child.localPosition.y, child.localPosition.z },
                        scale = new float[] { child.localScale.x, child.localScale.y, child.localScale.z },
                        color = new float[] { child.GetComponent<SpriteRenderer>().color.r,
                                          child.GetComponent<SpriteRenderer>().color.g,
                                          child.GetComponent<SpriteRenderer>().color.b },
                        rotation = new float[] { child.localRotation.eulerAngles.x, child.localRotation.eulerAngles.y, child.localRotation.eulerAngles.z }
                    })
                    .ToList();

        return new CharacterModel
        {
            shapes = shapes,
            textData = textData.text,
        };
    }
}
