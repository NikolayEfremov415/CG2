using UnityEngine;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    
    private float startTime;
    private bool isTimerRunning = false;
    private int returnCount = 0;
    private bool firstChoiceMade = false; // Следи дали изобщо е правен избор досега

    void Start()
    {
        if (timerText != null) timerText.text = "";
    }

    public void StartPuzzle()
    {
        // Стартът просто започва таймера чисто
        startTime = Time.time;
        isTimerRunning = true;
        
        UpdateUI(0);
        Debug.Log("Таймерът стартира!");
    }

    public void RegisterChoiceAndFinish()
    {
        // АКО ТАЙМЕРЪТ РАБОТИ: Това е нормално завършване на пъзела
        if (isTimerRunning)
        {
            isTimerRunning = false;
            firstChoiceMade = true;
            
            float finalTime = Time.time - startTime;
            Debug.Log("Първи избор направен! Време: " + finalTime);
            UpdateUI(finalTime);
        }
        // АКО ТАЙМЕРЪТ НЕ РАБОТИ, НО ВЕЧЕ СМЕ ПРАВИЛИ ИЗБОР: Това е промяна на избора (връщане)
        else if (firstChoiceMade)
        {
            returnCount++; // Увеличаваме само тук
            Debug.Log("Промяна на избора! Общо връщания: " + returnCount);
            
            // Автоматично рестартираме таймера за новия опит
            StartPuzzle();
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