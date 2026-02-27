using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ButtonTest : MonoBehaviour
{
    public Transform player;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SaveGame()
    {
        if (player == null)
        {
            Debug.LogError("Player reference missing!");
            return;
        }

        string sceneName = SceneManager.GetActiveScene().name;
        SaveSystem.Save(sceneName, player.position);
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        if (!SaveSystem.TryLoad(out GameSaveData data))
        {
            Debug.Log("No save file found.");
            return;
        }
        Debug.Log($"[LOAD] Loaded data scene='{data.sceneName}' pos=({data.px},{data.py},{data.pz})");
        StartCoroutine(LoadSceneAndPlacePlayer(data));
    }

    private IEnumerator LoadSceneAndPlacePlayer(GameSaveData data)
    {
        var operation = SceneManager.LoadSceneAsync(data.sceneName);

        while (!operation.isDone)
            yield return null;

        yield return null;

        if (player == null)
        {
            var found = GameObject.FindGameObjectWithTag("Player");
            if (found != null)
                player = found.transform;
        }

        if (player == null)
        {
            Debug.LogError("Player not found in loaded scene!");
            yield break;
        }

        player.position = new Vector3(data.px, data.py, data.pz);
    }
}