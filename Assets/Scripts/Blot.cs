using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System;

public class Blot : MonoBehaviour
{
    public string letter; // Letter to be written
    private SerialPort serialPort;
    private Thread serialThread;
    private bool isRunning;

    void Start()
    {
        // Initialize SerialPort
        serialPort = new SerialPort("/dev/tty.usbmodem101", 9600);
        serialPort.ReadTimeout = 500; // Set read timeout for non-blocking

        // Start the serial communication in a new thread
        isRunning = true;
        serialThread = new Thread(SerialThread);
        serialThread.Start();
    }

    void Update()
    {
        // Example of sending G-code commands
        if (serialPort.IsOpen)
        {
            serialPort.WriteLine("M4 S0");
            serialPort.WriteLine("G1 X0 Y0");
            serialPort.WriteLine("G1 X5 Y10");
            serialPort.WriteLine("G1 X10 Y0");
            serialPort.WriteLine("G1 X2.5 Y5");
            serialPort.WriteLine("G1 X7.5 Y5");
        }
    }

    void SerialThread()
    {
        try
        {
            // Open the serial port
            serialPort.Open();
            Debug.Log("Serial Port Opened");

            // Read any available response
            while (isRunning && serialPort.IsOpen)
            {
                try
                {
                    string response = serialPort.ReadLine();
                    Debug.Log(response); // Output the response from CNC
                }
                catch (TimeoutException)
                {
                    // Ignore timeouts and continue reading
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error in Serial Communication: " + e.Message);
        }
        finally
        {
            // Close the serial port safely
            if (serialPort.IsOpen)
            {
                serialPort.Close();
                Debug.Log("Serial Port Closed");
            }
        }
    }

    void OnApplicationQuit()
    {
        // Ensure the serial thread is stopped when the application quits
        isRunning = false;
        if (serialThread != null && serialThread.IsAlive)
        {
            serialThread.Join();
        }
    }
}
