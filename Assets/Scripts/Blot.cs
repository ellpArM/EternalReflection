using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class FlaskApiCaller : MonoBehaviour
{
    // URL of the Flask server (replace with your server's actual IP or domain)
    private const string flaskUrl = "http://10.66.95.154:5050/send_command/";

    // Function to call the Flask API with a G-code command
    public void SendCommand(string gCodeCommand)
    {
        // Start the coroutine to send the request
        StartCoroutine(SendCommandCoroutine(gCodeCommand));
    }

    private IEnumerator SendCommandCoroutine(string command)
    {
        // Construct the full URL with the command
        string requestUrl = flaskUrl + UnityWebRequest.EscapeURL(command);

        // Create a GET request
        using (UnityWebRequest request = UnityWebRequest.Get(requestUrl))
        {
            // Send the request and wait for the response
            yield return request.SendWebRequest();

            // Check for errors
            if (request.result == UnityWebRequest.Result.ConnectionError || 
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {request.error}");
            }
            else
            {
                // Successfully received a response
                Debug.Log($"Response: {request.downloadHandler.text}");
            }
        }
    }

    // Example usage: Call this function on start to test the connection
    private void Start()
    {
        // Example: Send a test command when the script initializes
        SendCommand("G0 X110 Y10"); // Replace with your desired G-code command
    }
}
