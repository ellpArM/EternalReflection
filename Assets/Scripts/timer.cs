using System;
using System.IO;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    // Variable to store total game time
    private float totalTime = 0f;

    // File path for saving the time (can be changed to a different location if needed)
    private string filePath = "game_time.txt";

    // Update is called once per frame
    void Update()
    {
        // Increment totalTime by the time passed since last frame
        totalTime += Time.deltaTime;
    }

    // Call this function when the game ends or when you want to save the time
    public void SaveTimeToFile()
    {
        // Format the total time to show minutes and seconds (optional)
        string formattedTime = TimeSpan.FromSeconds(totalTime).ToString(@"hh\:mm\:ss");

        // Write the formatted time to the file
        File.WriteAllText(filePath, formattedTime);

        // Log to the console for feedback
        Debug.Log("Game Time Saved: " + formattedTime);
    }

    // Optional: Call this when the game ends (e.g., on application quit)
    private void OnApplicationQuit()
    {
        SaveTimeToFile();
    }
}
