using System;
using System.IO;
using UnityEngine;
using Unity.Cinemachine; // Modern Cinemachine namespace
using UnityEngine.SceneManagement;

[Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    public string boundaryPath; // stable path in hierarchy
}

public class SaveController : MonoBehaviour
{
    [Header("References (drag in Inspector)")]
    [SerializeField] private Transform player;
    [SerializeField] private CinemachineConfiner2D confiner2D;

    private string saveLocation;

    private void Awake()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");

        // Fallbacks if you forget to assign in Inspector
        if (player == null)
        {
            GameObject p = GameObject.FindWithTag("Player");
            if (p != null) player = p.transform;
        }

        if (confiner2D == null)
        {
            confiner2D = FindFirstObjectByType<CinemachineConfiner2D>();
        }
    }

    private void Start()
    {
        LoadGame();
    }

    public void SaveGame()
    {
        if (player == null)
        {
            Debug.LogError("SaveGame failed: Player reference not set (and no object with tag 'Player' found).");
            return;
        }

        if (confiner2D == null || confiner2D.BoundingShape2D == null)
        {
            Debug.LogWarning("SaveGame: Confiner2D or its BoundingShape2D is missing. Saving player position only.");
        }

        SaveData saveData = new SaveData
        {
            playerPosition = player.position,
            boundaryPath = (confiner2D != null && confiner2D.BoundingShape2D != null)
                ? GetHierarchyPath(confiner2D.BoundingShape2D.transform)
                : ""
        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData, true));
        // Debug.Log("Saved to: " + saveLocation);
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        if (!File.Exists(saveLocation))
        {
            SaveGame(); // create initial save
            return;
        }

        if (player == null)
        {
            Debug.LogError("LoadGame failed: Player reference not set.");
            return;
        }

        SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
        player.position = saveData.playerPosition;

        // Restore boundary if possible
        if (!string.IsNullOrEmpty(saveData.boundaryPath) && confiner2D != null)
        {
            Transform t = FindByHierarchyPath(saveData.boundaryPath);
            if (t != null)
            {
                PolygonCollider2D poly = t.GetComponent<PolygonCollider2D>();
                if (poly != null)
                {
                    confiner2D.BoundingShape2D = poly;

                    // Optional: helps Cinemachine update cached confiner data
                    confiner2D.InvalidateCache();
                }
                else
                {
                    Debug.LogWarning($"LoadGame: Found boundary object but it has no PolygonCollider2D: {saveData.boundaryPath}");
                }
            }
            else
            {
                Debug.LogWarning($"LoadGame: Could not find boundary object path in scene: {saveData.boundaryPath}");
            }
        }
    }

    // ----- Helpers -----

    private static string GetHierarchyPath(Transform t)
    {
        // e.g. "Level/Bounds/CameraBoundary"
        string path = t.name;
        while (t.parent != null)
        {
            t = t.parent;
            path = t.name + "/" + path;
        }
        return path;
    }

    private static Transform FindByHierarchyPath(string path)
    {
        // Find root object first, then walk down children by name
        string[] parts = path.Split('/');
        if (parts.Length == 0) return null;

        GameObject rootObj = GameObject.Find(parts[0]);
        if (rootObj == null) return null;

        Transform current = rootObj.transform;
        for (int i = 1; i < parts.Length; i++)
        {
            Transform next = current.Find(parts[i]);
            if (next == null) return null;
            current = next;
        }
        return current;
    }
}