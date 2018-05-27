using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.Video;

public class TokenModerator : MonoBehaviour
{
    [SerializeField]
    private bool tokenIsPlaced = false;
    [SerializeField]
    private string COMPort = "";
    [SerializeField]
    private string position = "";
    private GameObject cube;
    private VideoPlayer videoPlayer;

    // supported baudrate is 9600
    private SerialPort serialPort;

    public GameObject Cube
    {
        get
        {
            return cube;
        }

        set
        {
            cube = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        if (COMPort != "")
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

        if (Cube != null)
        {
            videoPlayer = Cube.GetComponent<VideoPlayer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (COMPort != "" && Cube != null)
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
                                tokenIsPlaced = true;
                                Cube.SetActive(tokenIsPlaced);
                                BuzzerAction();
                                videoPlayer.Play();
                                break;

                            case "0":
                                tokenIsPlaced = false;
                                Cube.SetActive(tokenIsPlaced);
                                videoPlayer.Stop();
                                break;

                            default:
                                Debug.LogWarning("COMPort " + COMPort + " - Ignored Output from Arduino: " + output);
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

    // TODO: implement (animation)
    private void BuzzerAction()
    {
        Debug.LogError("MODERATOR BUZZER!");
    }
}
