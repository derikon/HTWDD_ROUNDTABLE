using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.Video;
using System;

public class TokenModerator : MonoBehaviour
{
    public GameObject screen;
    public VideoClip[] videoClips;
    VoiceParticleSystem voiceParticleSystem;

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
        videoPlayer = GetComponentInParent<VideoPlayer>();
        videoPlayer.isLooping = true;
        videoPlayer.clip = videoClips[0];
        videoPlayer.Play();

        buzzer_bg = GameObject.FindGameObjectWithTag("Buzzer_BG");

        triggerTime = DateTime.Now;

        voiceParticleSystem = GetComponentInChildren<VoiceParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: just for debugging reasons, delete afterwards
        if (Input.GetKeyUp("down"))
        {
            InitialTokenAction();
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            BuzzerAction();
        }

        if (buzzer_bg == null)
            return;

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
                        Debug.Log("Moderator Token is placed!");
                        tokenIsPlaced = true;
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
        videoPlayer.clip = videoClips[2];
        videoPlayer.Prepare();
        videoPlayer.isLooping = false;

        triggerTime = DateTime.Now;
        videoPlayer.Play();
        screen.GetComponent<FadeInOut>().FadeIn(0);
        screen.GetComponent<FadeInOut>().FadeOut(4);

        // Fade in background
        buzzer_bg.GetComponent<FadeInOut>().FadeIn();
    }

    //TODO: implement
    private void InitialTokenAction()
    {
        Debug.Log("Hallo Moderator!");
        screen.GetComponent<FadeInOut>().FadeOut(5);
        voiceParticleSystem.enabled = true;
    }
}
