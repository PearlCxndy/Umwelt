using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;
using TMPro;

public class CubeEncoder : MonoBehaviour
{
    public string portName = "COM3";
    public int baudRate = 9600;
    public float rotationPerStep = 1f;

    [HideInInspector]
    public float encoderAngle = 0f;

    private SerialPort serialPort;
    private int currentCount = 0;
    public TextMeshPro monitorText;

    void Start()
    {
        try
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.Open();
            serialPort.ReadTimeout = 50;
            Debug.Log("Serial port opened.");
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning("Could not open serial port: " + ex.Message);
        }
    }

    void Update()
    {
        if (monitorText != null)
        {
            monitorText.text = $"Encoder Count: {currentCount}\nAngle: {encoderAngle:F2}";
        }

        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string line = serialPort.ReadLine();
                currentCount = int.Parse(line.Trim());

                // Calculate angle and expose it
                encoderAngle = currentCount * (rotationPerStep * 10);
                Debug.Log("Encoder Count: " + currentCount + " | Angle: " + encoderAngle);
            }
            catch (System.TimeoutException) { }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Serial read error: " + ex.Message);
            }
        }
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
