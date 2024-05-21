using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] private Transform characterModelParent;
    [SerializeField] private TMP_InputField textData;

    public void ResetCharacter()
    {
        foreach (Transform child in characterModelParent.transform)
        {
            Destroy(child.gameObject);
        }
        textData.text = string.Empty;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
