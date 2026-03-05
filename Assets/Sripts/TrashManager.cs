using UnityEngine;
using TMPro;

public class TrashManager : MonoBehaviour
{
    public TextMeshProUGUI trashText; 
    private int collectedTrash = 0;

    void Start()
    {
        UpdateTrashUI();
    }

    public void CollectTrash()
{
    collectedTrash++;
    Debug.Log("БОКЛУК СЪБРАН! Текущ брой: " + collectedTrash); // Това ще излезе в Console
    UpdateTrashUI();
}
    void UpdateTrashUI()
    {
        if (trashText != null)
            trashText.text = "Изчистен боклук: " + collectedTrash;
    }
}