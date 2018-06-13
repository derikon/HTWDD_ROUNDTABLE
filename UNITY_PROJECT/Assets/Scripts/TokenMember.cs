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
    [SerializeField]
    private VideoPlayer videoPlayer;
    public VideoClip[] videoClips;

    VoiceParticleSystem voiceParticleSystem;

    [SerializeField]
    private bool initialPlacement = true;

    // supported baudrate is 9600
    private SerialPort serialPort;

    // Use this for initialization
    void Start()
    {
        videoPlayer = screen.GetComponentInParent<VideoPlayer>();
        videoPlayer.isLooping = true;
        videoPlayer.clip = videoClips[0];
        videoPlayer.Play();
        voiceParticleSystem = GetComponentInChildren<VoiceParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp("down"))
        {
            InitialTokenAction();
        }

        if (Input.GetKeyUp("up"))
        {
            SpeechRequest();
        }

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
                        if (initialPlacement)
                        {
                            Debug.Log("INITIAL PLACEMENT ON POS " + Position);
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
            serialPort.ReadTimeout = 10;
            return true;
        }
        catch (System.Exception)
        {
            Debug.LogError("Cannot open Port " + COMPort + "! It might be eventually used.");
            return false;
        }
    }

    public bool TokenIsPlaced()
    {
        return tokenIsPlaced;
    }

    //TODO: implement
    private void InitialTokenAction()
    {
        Debug.Log("Hallo du auf Position " + Position + "!");
        screen.GetComponent<FadeInOut>().FadeOut(5);
        voiceParticleSystem.enabled = true;
    }

    //TODO: implement
    private void SpeechRequest()
    {
        videoPlayer.clip = videoClips[2];
        videoPlayer.Prepare();
        videoPlayer.isLooping = false;

        Debug.Log("Redewunsch von Member " + Position + ".");
        videoPlayer.Play();
        screen.GetComponent<FadeInOut>().FadeIn(0);
        screen.GetComponent<FadeInOut>().FadeOut(4);
    }
}
