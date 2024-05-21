using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCanvasPage : MonoBehaviour
{
    [SerializeField] private GameObject[] canvasToPassive;
    [SerializeField] private GameObject[] canvasToActive;

    public void Switch(bool activeCanvas = true)
    {
        foreach (GameObject canvas in canvasToPassive) canvas.SetActive(!activeCanvas);
        foreach (GameObject canvas in canvasToActive) canvas.SetActive(activeCanvas);
    }
}
