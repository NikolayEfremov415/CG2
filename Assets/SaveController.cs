using UnityEngine;
using System.IO;
using Cinemachine;
public class SaveController : MonoBehaviour
{
    private string saveLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "savefile.dat"); // Set the save location to a file in the persistent data path
        LoadGame(); // Load the game data when the game starts
    }

    // Update is called once per frame
    public void SaveGame()
    {
        SaveData data = new SaveData() // Create a new instance of the SaveData class
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position, // Save the player's position
            mapBoundary = FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D.gameObject.name // Save the name of the map boundary
        };
        
        File.WriteAllText(saveLocation, JsonUtility.ToJson(data)); // Write the save data to a file in JSON format
    }
    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData savedData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation)); // Read the save data from the file and deserialize it from JSON format
            GameObject.FindGameObjectWithTag("Player").transform.position = savedData.playerPosition; // Set the player's position to the saved position
            FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D = GameObject.Find(savedData.mapBoundary).GetComponent<PolygonCollider2D>(); // Set the map boundary to the saved boundary
        }
        else
        {
            SaveGame(); // If the save file does not exist, create a new one
        }
    }
}
