using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateModel : MonoBehaviour
{
    [SerializeField] private Transform CharacterModelParent;
    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private GameObject circlePrefab;

    private int nextOrderInLayer = 1;

    private readonly Stack<GameObject> undoStack = new();
    private readonly Stack<GameObject> redoStack = new();


    private void Start()
    {
        CharacterModelParent.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        undoStack.Clear();
        redoStack.Clear();
    }

    public void AddSquare()
    {
        GameObject square = Instantiate(squarePrefab, RandomPosition(), Random.rotation, CharacterModelParent);
        square.transform.localScale = RandomScale();
        square.GetComponent<SpriteRenderer>().color = RandomColor();
        SpriteRenderer sr = square.GetComponent<SpriteRenderer>();
        sr.sortingOrder = nextOrderInLayer++;
        undoStack.Push(square);
    }

    public void AddCircle()
    {
        GameObject circle = Instantiate(circlePrefab, RandomPosition(), Random.rotation, CharacterModelParent);
        circle.transform.localScale = RandomScale();
        circle.GetComponent<SpriteRenderer>().color = RandomColor();
        SpriteRenderer sr = circle.GetComponent<SpriteRenderer>();
        sr.sortingOrder = nextOrderInLayer++;
        undoStack.Push(circle);
    }

    public void Undo()
    {
        if (undoStack.Count > 0)
        {
            GameObject lastObject = undoStack.Pop();
            lastObject.SetActive(false);
            redoStack.Push(lastObject);
        }
    }

    public void Redo()
    {
        if (redoStack.Count > 0)
        {
            GameObject lastObject = redoStack.Pop();
            lastObject.SetActive(true);
            undoStack.Push(lastObject);
        }
    }

    Vector3 RandomPosition()
    {
        Bounds bounds = CharacterModelParent.GetComponent<SpriteRenderer>().bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(x, y, 0);
    }

    Vector3 RandomScale()
    {
        float scale = Random.Range(0.5f, 1.5f);
        return new Vector3(scale, scale, 0);
    }

    Color RandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }

}
