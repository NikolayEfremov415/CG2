using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

    public static void Save(string sceneName, Vector3 playerPos)
    {
        var data = new GameSaveData
        {
            sceneName = sceneName,
            px = playerPos.x,
            py = playerPos.y,
            pz = playerPos.z
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);

        Debug.Log("Game Saved!");
    }

    public static bool TryLoad(out GameSaveData data)
    {
        data = null;

        if (!File.Exists(SavePath))
            return false;

        string json = File.ReadAllText(SavePath);
        data = JsonUtility.FromJson<GameSaveData>(json);

        return data != null && !string.IsNullOrEmpty(data.sceneName);
    }

    public static void DeleteSave()
    {
        if (File.Exists(SavePath))
            File.Delete(SavePath);
    }
}