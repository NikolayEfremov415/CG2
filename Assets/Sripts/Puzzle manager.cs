using UnityEngine;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Плъзни текстовия обект тук
    
    private float startTime;
    private bool isTimerRunning = false;
    private bool hasFinishedOnce = false; 
    private int returnCount = 0;

    void Start()
    {
        if (timerText != null) timerText.text = ""; // Изчиства "New Text" в началото
    }

    public void StartPuzzle()
    {
        // Ако вече сме минали през избор и се върнем на старта, броим го за връщане
        if (hasFinishedOnce)
        {
            returnCount++;
        }

        startTime = Time.time;
        isTimerRunning = true;
        hasFinishedOnce = false; 
        
        UpdateUI(0);
        Debug.Log("Таймерът стартира! Връщания: " + returnCount);
    }

    public void RegisterChoiceAndFinish()
    {
        if (isTimerRunning)
        {
            isTimerRunning = false;
            hasFinishedOnce = true; 
            
            float finalTime = Time.time - startTime;
            if (timerText != null)
            {
                timerText.text = string.Format("Избор направен!\nВреме: {0:0.00}с\nВръщания: {1}", finalTime, returnCount);
            }
        }
    }

    void Update()
    {
        if (isTimerRunning && timerText != null)
        {
            float t = Time.time - startTime;
            UpdateUI(t);
        }
    }

    void UpdateUI(float time)
    {
        if (timerText != null)
        {
            timerText.text = string.Format("Време: {0:0.00}с\nВръщания: {1}", time, returnCount);
        }
    }
}