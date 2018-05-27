using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.Video;

public class ArduinoPosition : MonoBehaviour
{ 
    [SerializeField]
    private bool tokenIsPlaced = false;
    [SerializeField]
    private string COMPort = "";
    [SerializeField]
    private string position = "";
    [SerializeField]
    GameObject cube;
    private VideoPlayer videoPlayer;

    // supported baudrate is 9600
    private SerialPort serialPort;

    // Use this for initialization
    void Start()
    {
        if(COMPort != "")
        {
            serialPort = new SerialPort("\\\\.\\COM" + COMPort, 9600);
            try
            {
                serialPort.Open();
            }
            catch (System.Exception)
            {
                Debug.LogError("Cannot open Port " + COMPort + "! It might be eventually used.");
                throw;
            }
            serialPort.ReadTimeout = 10;
        }
        else
        {
            Debug.LogError("Port undefined!");
        }

        if (cube != null)
        {
            Debug.Log("Port " + COMPort + " - cube set");
            videoPlayer = cube.GetComponent<VideoPlayer>();
            Debug.Log("VideoPlayer: " + videoPlayer.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (COMPort != "" && cube != null)
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
                                Debug.Log("Token on Position " + position + " is placed!");
                                tokenIsPlaced = true;
                                cube.SetActive(tokenIsPlaced);
                                Debug.Log("VideoPlayer PLAY: " + videoPlayer.ToString());
                                videoPlayer.Play();
                                break;

                            case "0":
                                Debug.Log("Token on Position " + position + " is removed!");
                                tokenIsPlaced = false;
                                //cube.SetActive(tokenIsPlaced);
                                videoPlayer.Stop();
                                break;

                            default:
                                Debug.LogWarning("COMPort " + COMPort + " - Unkown Output from Arduino: " + output);
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

    public bool TokenIsPlaced()
    {
        return tokenIsPlaced;
    }
}
