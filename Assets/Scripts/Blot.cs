using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System;
using System.IO.Ports;

public class Blot : MonoBehaviour
{
    public string letter; // Letter to be written
    private SerialPort serialPort;

    void Start()
    {
        serialPort = new SerialPort("/dev/tty.usbmodem101", 9600); // COM port and baud rate
        serialPort.Open();
        // mainfunction(letter); // Pass the letter to the main function
        serialPort.WriteLine("M4 S0");
        serialPort.WriteLine("G1 X0 Y0");
        serialPort.WriteLine("G1 X5 Y10");
        serialPort.WriteLine("G1 X10 Y0");
        
        // serialPort.WriteLine("M3 S180");
        serialPort.WriteLine("G1 X2.5 Y5");
        serialPort.WriteLine("M4 S0");
        serialPort.WriteLine("G1 X7.5 Y5");
        serialPort.WriteLine("M3 S180");
        
        // debugging
        string response = serialPort.ReadLine();
        Debug.Log(response); // Output the response from CNC

        serialPort.Close();
    }

    public void mainfunction(string letter)
    {
        // Letter A
        // if (letter.ToLower() == "a") {
        serialPort.WriteLine("M3 S100");
        serialPort.WriteLine("G1 X0 Y0");
        serialPort.WriteLine("G1 X5 Y10");
        serialPort.WriteLine("G1 X10 Y0");
        serialPort.WriteLine("G1 X2.5 Y5");
        serialPort.WriteLine("G1 X7.5 Y5");
        serialPort.WriteLine("M3 S100");
        serialPort.WriteLine("M5");
        // }
    }
}