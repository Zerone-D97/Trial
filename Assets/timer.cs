using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private float remainingTime = 60f; // Default starting time
    public bool GameActive { get; private set; } = true;
    public GameObject GameOverCanvas;

    void Start()
    {
        UpdateTimerText();
    }

    void Update()
    {
        if (GameActive)
        {
            // Reduce remaining time
            remainingTime -= Time.deltaTime;

            // Check if the timer has reached zero
            if (remainingTime <= 0)
            {
                remainingTime = 0; // Prevent negative time
                GameOverCanvas.SetActive(true);
                GameActive = false;
                OnTimerEnd(); // Call method when timer ends
            }

            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        // Update the timer text
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void OnTimerEnd()
    {
        // Add logic for what happens when the timer ends
        Debug.Log("Timer has ended!");
    }

    public void PauseTimer()
    {
        GameActive = false;
    }

    public void ResumeTimer()
    {
        GameActive = true;
    }

    public void ResetTimer(float time)
    {
        remainingTime = time;
        GameActive = true;
        UpdateTimerText();
    }
}
