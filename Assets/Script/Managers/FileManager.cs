using System;
using System.IO;
using System.Text;
using UnityEngine;

public class FileManager
{
    public static event Action<string> OnSaveSuccess;
    public static event Action<string> OnSaveError;
    public static event Action<string> OnLoadError;
    public static event Action<string> OnDeleteSuccess;
    public static event Action<string> OnDeleteError;

    private const string FileExtension = ".json";
    private readonly string basePath = Application.persistentDataPath + "/characterModel_";

    public void SaveToFile(string jsonData, int savingIndex, bool encrypt = false)
    {
        string path = basePath + savingIndex + FileExtension;
        if (encrypt && savingIndex >= 2)
        {
            jsonData = EncryptionUtility.EncryptString(jsonData);
        }

        if (IsEnoughSpace(jsonData))
        {
            File.WriteAllText(path, jsonData);
            OnSaveSuccess?.Invoke("Character Model Saved Succesfully");
        }
        else
        {
            OnSaveError?.Invoke("Not enough storage space to save the JSON data.");
        }
    }

    public string LoadFromFile(int savingIndex, bool decrypt = false)
    {
        string path = basePath + savingIndex + FileExtension;
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            if (decrypt && savingIndex >= 2)
            {
                jsonData = EncryptionUtility.DecryptString(jsonData);
            }
            return jsonData;
        }
        OnLoadError?.Invoke("Save file not found!");
        return null;
    }

    public void DeleteFile(int savingIndex)
    {
        string path = basePath + savingIndex + FileExtension;
        if (File.Exists(path))
        {
            File.Delete(path);
            OnDeleteSuccess?.Invoke("File deleted successfully");
        }
        else
        {
            OnDeleteError?.Invoke("Failed to delete files");
        }
    }

    private bool IsEnoughSpace(string data)
    {
        long jsonDataSize = Encoding.UTF8.GetByteCount(data);
        DriveInfo driveInfo = new(Path.GetPathRoot(Application.persistentDataPath));
        return driveInfo.IsReady && driveInfo.AvailableFreeSpace > jsonDataSize;
    }

    public string GetFilePath(int index)
    {
        return basePath + index + FileExtension;
    }
}
