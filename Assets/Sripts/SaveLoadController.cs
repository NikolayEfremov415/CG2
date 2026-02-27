using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SaveLoadController : MonoBehaviour
{
    public Transform player;

    public void SaveGame()
    {
        if (player == null)
        {
            Debug.LogError("Player reference missing!");
            return;
        }

        SaveSystem.Save(SceneManager.GetActiveScene().name, player.position);
    }

    public void LoadGame()
    {
        if (!SaveSystem.TryLoad(out GameSaveData data))
        {
            Debug.Log("No save found.");
            return;
        }

        StartCoroutine(LoadSceneAndPlacePlayer(data));
    }

    private IEnumerator LoadSceneAndPlacePlayer(GameSaveData data)
    {
        var op = SceneManager.LoadSceneAsync(data.sceneName);
        while (!op.isDone) yield return null;
        yield return null;

        if (player == null)
        {
            var found = GameObject.FindGameObjectWithTag("Player");
            if (found != null) player = found.transform;
        }

        if (player == null)
        {
            Debug.LogError("Player not found!");
            yield break;
        }

        player.position = new Vector3(data.px, data.py, data.pz);
    }
}