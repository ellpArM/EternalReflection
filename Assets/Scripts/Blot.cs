/*using System;
using System.IO.Ports;
using UnityEngine;

public class CNCSender : MonoBehaviour
{
    public string portName = "/dev/tty.usbmodem101"; // Replace with your CNC machine's COM port
    public int baudRate = 115200; // Baud rate for CNC machine
    private SerialPort serialPort;
    private float moveDistance = 10f; // Movement distance for each key press

    void Update()
    {
        // Listen for key presses (WASD) and move the CNC machine accordingly
        if (Input.GetKeyDown(KeyCode.W)) SendGCode(0, moveDistance); // Move up
        if (Input.GetKeyDown(KeyCode.A)) SendGCode(-moveDistance, 0); // Move left
        if (Input.GetKeyDown(KeyCode.S)) SendGCode(0, -moveDistance); // Move down
        if (Input.GetKeyDown(KeyCode.D)) SendGCode(moveDistance, 0); // Move right
    }

    // Function to generate and send the G-code based on movement
    void SendGCode(float moveX, float moveY)
    {
        string gcode = GenerateGCode(moveX, moveY);
        SendGCodeToCNC(gcode);
    }

    // Function to create the G-code string
    string GenerateGCode(float moveX, float moveY)
    {
        // Start G-code with some basic setup
        string gcode = "G21 ; Set units to millimeters\n";  // Set units to mm
        gcode += "G90 ; Use absolute positioning\n"; // Absolute positioning

        // Add movement instruction (G0 for rapid move)
        gcode += $"G0 X{moveX} Y{moveY} Z0.00 ; Move to X{moveX} Y{moveY} Z0.00\n";

        return gcode;
    }

    // Function to send the G-code to the CNC machine via serial port
    void SendGCodeToCNC(string gcode)
    {
        try
        {
            // Open serial port if it's not already open
            if (serialPort == null)
            {
                serialPort = new SerialPort(portName, baudRate);
                serialPort.Open();
            }

            // Send the generated G-code
            serialPort.WriteLine(gcode);
            Debug.Log($"Sent: {gcode}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error sending G-code: {e.Message}");
        }
    }
}
*/