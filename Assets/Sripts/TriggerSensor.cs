using UnityEngine;

public class TriggerSensor : MonoBehaviour
{
    [Header("Настройки на сензора")]
    public bool isStart = false; 

    [Header("Връзка с Мениджъра")]
    public PuzzleManager manager; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяваме дали обекта има таг Player
        if (other.CompareTag("Player") && manager != null)
        {
            if (isStart)
            {
                manager.StartPuzzle();
            }
            else
            {
                manager.RegisterChoiceAndFinish();
            }
        }
    }
}