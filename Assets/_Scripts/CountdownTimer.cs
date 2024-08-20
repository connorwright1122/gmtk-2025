using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 60f; // Time in seconds for the countdown
    public TextMeshProUGUI countdownText; // Reference to the TextMeshProUGUI component
    private bool timerIsRunning = false;

    void Start()
    {
        // Start the timer automatically
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay("Ring shrinking in: ", timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                UpdateTimerDisplay("Kaijumo!");
            }
        }
    }

    void UpdateTimerDisplay(string preText, float timeToDisplay)
    {
        // Format the time into minutes and seconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Update the TextMeshProUGUI text component
        countdownText.text = preText + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void UpdateTimerDisplay(string preText)
    {
        // Format the time into minutes and seconds
        //float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        //float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Update the TextMeshProUGUI text component
        countdownText.text = preText;
    }
}
