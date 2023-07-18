using SuperMaxim.Messaging;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float totalTime = 60f; // Total time in seconds
    //public Slider timerSlider; // Reference to the slider
    public TMP_Text timerText; // Reference to the text displaying time
    public TMP_Text timerText2;

    private float currentTime; // Current time in seconds
    private bool timerStopped = false;

    [Header("Debug")]
    public int minutes;
    public int seconds;

    private void Start()
    {
        currentTime = totalTime;
        UpdateTimerUI();
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();
        }
        else if (!timerStopped)
        {
            currentTime = 0;
            UpdateTimerUI();
            // Timer has reached 0, perform any actions needed
            // (e.g., end game, trigger an event, etc.)
            Messenger.Default.Publish(new EndGamePayload());
            timerStopped = true;
        }
    }

    private void UpdateTimerUI()
    {
        // Calculate minutes and seconds
        minutes = Mathf.FloorToInt(currentTime / 60f);
        seconds = Mathf.FloorToInt(currentTime % 60f);

        // Update the slider value (normalized between 0 and 1)
        //timerSlider.value = 1 - (currentTime / totalTime);

        // Update the text displaying time
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (timerText2.isActiveAndEnabled)
        {
            timerText2.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
