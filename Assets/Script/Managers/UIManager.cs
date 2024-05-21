using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject dialogPrefab;
    [SerializeField] private Transform dialogParent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateSaveSlotUI(Transform saveModelSlotButton, int index, bool isSaved, string lastModifiedDate = "")
    {
        Transform saveSlot = saveModelSlotButton.GetChild(index).GetChild(0);
        if (isSaved)
        {
            saveSlot.gameObject.SetActive(true);
            saveSlot.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(lastModifiedDate);
            saveModelSlotButton.GetChild(index).GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            saveSlot.gameObject.SetActive(false);
            saveModelSlotButton.GetChild(index).GetChild(1).gameObject.SetActive(true);
        }
    }

    public void ShowDialog(string message)
    {
        GameObject dialogInstance = Instantiate(dialogPrefab, dialogParent);
        dialogInstance.GetComponentInChildren<TextMeshProUGUI>().text = message;
        Destroy(dialogInstance, 3f); // Auto-destroy after 3 seconds
    }
}
