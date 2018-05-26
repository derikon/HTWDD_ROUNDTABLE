using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Arduino_Pos1 : MonoBehaviour
{

    private Color currentColor;
    private Material materialColored;

    public Color ObjectColor;    
    public bool tokenIsPlaced = false;

    // supported baudrate is 9600
    // TODO: COM Port needs to be adjusted for all positions !
    private SerialPort serialPort = new SerialPort("\\\\.\\COM21", 9600);

    // Use this for initialization
    void Start()
    {
        serialPort.Open();
        serialPort.ReadTimeout = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                string output = serialPort.ReadLine();
                if (output != tokenIsPlaced.ToString())
                {
                    switch (output)
                    {
                        case "1":
                            Debug.Log("Token is placed!");
                            tokenIsPlaced = true;
                            break;

                        case "0":
                            Debug.Log("Token is removed!");
                            tokenIsPlaced = false;
                            break;

                        default:
                            Debug.Log("[WARNING] Unkown Output from Arduino: " + output);
                            break;
                    }

                }
            }
            catch (System.Exception)
            {
                // Nothing to do here, because else the log would be flooded
            }
        }
    }
}
