using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.Video;
using System;

public class TokenModerator : MonoBehaviour
{
    public GameObject screen;

    GameObject buzzer_bg;
    DateTime triggerTime;
    TimeSpan timeSpan = new TimeSpan(0, 0, 5);

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

        buzzer_bg = GameObject.FindGameObjectWithTag("Buzzer_BG");

        triggerTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: just for debugging reasons, delete afterwards
        if (Input.GetKeyUp(KeyCode.F))
        {
            BuzzerAction();
        }

        if (buzzer_bg == null) return;

        if ((DateTime.Now).Subtract(triggerTime) > timeSpan && buzzer_bg.GetComponent<FadeInOut>().isFadedOut == false)
        {
            buzzer_bg.GetComponent<FadeInOut>().FadeOut();
        }

        if (serialPort == null)
            return;


        if (string.IsNullOrEmpty(COMPort) || !serialPort.IsOpen)
            return;

        try
        {
            string output = serialPort.ReadLine();
            if (output != tokenIsPlaced.ToString())
            {
                switch (output)
                {
                    case "true":
                        tokenIsPlaced = true;
                        //screen.SetActive(true);
                        //Cube.SetActive(tokenIsPlaced);
                        videoPlayer.Play();
                        if (initialPlacement)
                        {
                            initialPlacement = false;
                            InitialTokenAction();
                        }
                        else
                        {
                            BuzzerAction();
                        }
                        break;

                    case "false":
                        tokenIsPlaced = false;
                        //screen.SetActive(false);
                        //Cube.SetActive(tokenIsPlaced);
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
            //throw;
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

    // TODO: implement (animation)
    void BuzzerAction()
    {
        Debug.LogError("MODERATOR BUZZER!");
        triggerTime = DateTime.Now;
        buzzer_bg.GetComponent<FadeInOut>().FadeIn();
    }

    //TODO: implement
    private void InitialTokenAction()
    {
        Debug.Log("Hallo du auf Position " + Position + "!");
        //Debug.LogWarning("[TokenMember.cs] InitialTokenAction() not implemented!");
    }
}
