using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeArea : MonoBehaviour
{
    public Text scoreText; // Reference to the UI text for displaying the score
    public static int scoreCount; // Static variable to hold the score

    private void Start()
    {
        // Initialize the score display
        UpdateScoreDisplay();
    }

    // Trigger method
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Victim" tag
        if (other.CompareTag("Victim"))
        {
            Destroy(other.gameObject); // Destroy the GameObject
            scoreCount++; // Increment the score

            UpdateScoreDisplay(); // Update the score display
        }
    }

    // Method to update the score display
    private void UpdateScoreDisplay()
    {
        scoreText.text = "VICTIMSAVED:" + scoreCount.ToString();
    }
}
