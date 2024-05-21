using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class SaveLoadModel : MonoBehaviour
{
    [SerializeField] private Transform saveModelSlotButton;
    [SerializeField] private GameObject characterModelParent;
    [SerializeField] private TMP_InputField textData;

    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private GameObject circlePrefab;


    private int savingIndex = 0;
    private int saveSlotAvailable = 4;

    private Color selectedColor = new(0.67f, 0.67f, 1);
    private Color whiteColor = new(1, 1, 1);

    public static int nextOrderInLayer = 1;

    private CharacterModelDataHandler dataHandler;
    private FileManager fileManager;
    private UIManager uiManager;

    private void Awake()
    {
        dataHandler = new CharacterModelDataHandler();
        fileManager = new FileManager();
        uiManager = UIManager.Instance;
    }

    private void OnEnable()
    {
        saveSlotAvailable = saveModelSlotButton.childCount;
        LoadSavedFile();

        FileManager.OnSaveSuccess += HandleSaveSuccess;
        FileManager.OnSaveError += HandleSaveError;
        FileManager.OnLoadError += HandleLoadError;
        FileManager.OnDeleteSuccess += HandleDeleteSuccess;
        FileManager.OnDeleteError += HandleDeleteError;
    }

    private void OnDisable()
    {
        FileManager.OnSaveSuccess -= HandleSaveSuccess;
        FileManager.OnSaveError -= HandleSaveError;
        FileManager.OnLoadError -= HandleLoadError;
        FileManager.OnDeleteSuccess -= HandleDeleteSuccess;
        FileManager.OnDeleteError -= HandleDeleteError;
    }

    private void HandleSaveSuccess(string message)
    {
        UIManager.Instance.ShowDialog(message);
    }

    private void HandleSaveError(string message)
    {
        UIManager.Instance.ShowDialog(message);
    }

    private void HandleLoadError(string message)
    {
        UIManager.Instance.ShowDialog(message);
    }

    private void HandleDeleteSuccess(string message)
    {
        UIManager.Instance.ShowDialog(message);
    }

    private void HandleDeleteError(string message)
    {
        UIManager.Instance.ShowDialog(message);
    }

    private void LoadSavedFile()
    {
        for (int i = 0; i < saveSlotAvailable; i++)
        {
            string fullPath = fileManager.GetFilePath(i);
            bool isSaved = File.Exists(fullPath);
            string lastModifiedDate = isSaved ? File.GetLastWriteTime(fullPath).ToString("yyyy-MM-dd HH:mm:ss") : "";

            uiManager.UpdateSaveSlotUI(saveModelSlotButton, i, isSaved, lastModifiedDate);
        }
    }

    public void SaveSlotClick(int index)
    {
        savingIndex = index;
        for (int i = 0; i < saveSlotAvailable; i++)
        {
            saveModelSlotButton.GetChild(i).GetComponent<Image>().color = whiteColor;
        }
        saveModelSlotButton.GetChild(index).GetComponent<Image>().color = selectedColor;
    }

    public void SaveCharacterModel()
    {
        CharacterModel data = dataHandler.CollectCharacterData(characterModelParent.transform, textData);
        string json = JsonUtility.ToJson(data);
        fileManager.SaveToFile(json, savingIndex, savingIndex >= 2);
        LoadSavedFile();
    }

    public void DeleteSavedJson()
    {
        fileManager.DeleteFile(savingIndex);
        LoadSavedFile();
    }

    public void LoadCharacterModel()
    {
        string jsonData = fileManager.LoadFromFile(savingIndex, savingIndex >= 2);
        if (!string.IsNullOrEmpty(jsonData))
        {
            CharacterModel data = JsonUtility.FromJson<CharacterModel>(jsonData);
            ConvertCharacterModel(data);
            GetComponent<SwitchCanvasPage>().Switch();
        }
    }

    private void ConvertCharacterModel(CharacterModel data)
    {
        ClearModel();

        foreach (ShapeDataModel shape in data.shapes)
        {
            GameObject prefab = shape.type == "square" ? squarePrefab : circlePrefab;
            GameObject shapeObject = Instantiate(prefab, characterModelParent.transform);

            shapeObject.transform.localPosition = new Vector3(shape.position[0], shape.position[1], shape.position[2]);
            shapeObject.transform.localScale = new Vector3(shape.scale[0], shape.scale[1], shape.scale[2]);
            shapeObject.GetComponent<SpriteRenderer>().color = new Color(shape.color[0], shape.color[1], shape.color[2]);
            shapeObject.transform.localRotation = Quaternion.Euler(shape.rotation[0], shape.rotation[1], shape.rotation[2]);
            shapeObject.GetComponent<SpriteRenderer>().sortingOrder = ++nextOrderInLayer;
        }

        textData.text = data.textData;
    }

    private void ClearModel()
    {
        nextOrderInLayer = 1;
        foreach (Transform child in characterModelParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
