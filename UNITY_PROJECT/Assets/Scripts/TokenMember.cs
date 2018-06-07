using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.Video;

public class TokenMember : MonoBehaviour
{
    public GameObject screen;


    [SerializeField]
    private bool tokenIsPlaced = false;

    private string comPort;
    public string COMPort
    {
        get { return comPort; }
        set
        {
            if (comPort == value) return;
            comPort = value;
        }
    }

    public int Position
    {
        get
        {
            return position;
        }

        set
        {
            position = value;
        }
    }

    [SerializeField]
    private int position;
    private VideoPlayer videoPlayer;

    [SerializeField]
    private bool initialPlacement = true;

    // supported baudrate is 9600
    private SerialPort serialPort;

    // Use this for initialization
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (serialPort == null)
            return;

        if (string.IsNullOrEmpty(COMPort) || !serialPort.IsOpen) return;

        try
        {
            string output = serialPort.ReadLine();
            if (output != tokenIsPlaced.ToString())
            {
                switch (output)
                {
                    case "true":
                        Debug.Log("Token on Position " + Position + " is placed!");
                        tokenIsPlaced = true;
                        //screen.SetActive(true);
                        videoPlayer.Play();
                        if (initialPlacement)
                        {
                            initialPlacement = false;
                            InitialTokenAction();
                        }
                        else
                        {
                            SpeechRequest();
                        }

                        break;

                    case "false":
                        Debug.Log("Token on Position " + Position + " is removed!");
                        tokenIsPlaced = false;
                        //screen.SetActive(false);
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

    public bool InitializeComPort(string comPort)
    {
        if (string.IsNullOrEmpty(comPort)) return false;

        COMPort = comPort;
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
        return true;
    }

    public bool TokenIsPlaced()
    {
        return tokenIsPlaced;
    }

    //TODO: implement
    private void InitialTokenAction()
    {
        Debug.Log("Hallo du auf Position " + Position + "!");
        //Debug.LogWarning("[TokenMember.cs] InitialTokenAction() not implemented!");
    }

    //TODO: implement
    private void SpeechRequest()
    {
        Debug.Log("Redewunsch von Member " + Position + ".");
        //Debug.LogWarning("[TokenMember.cs] SpeechRequest() not implemented!");
    }
}
