using System.IO;
using UnityEditor;
using UnityEngine;

public static class PlayerSaveMenu
{
    private const string FileName = "PlayerSave.json";

    [MenuItem("Tools/Delete Player Save")]
    private static void DeleteSave()
    {
        string path = Path.Combine(Application.persistentDataPath, FileName);

        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log($"Save deleted: {path}");
        }
        else
        {
            Debug.Log($"No save found at: {path}");
        }
    }
}
