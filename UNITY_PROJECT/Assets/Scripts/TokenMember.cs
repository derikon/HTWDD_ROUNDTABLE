using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.Video;

public class TokenMember : MonoBehaviour
{
    public GameObject screen;
    public TextMesh textmesh;

    public ParticleSystem particleSystemRing;
    public MicVisualizer voiceVisualizer;

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

    //VoiceParticleSystem voiceParticleSystem;

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
        voiceVisualizer.enabled = false;
        //voiceParticleSystem = GetComponentInChildren<VoiceParticleSystem>();
    }

    private void useKeyboard()
    {
        //if (Input.GetKeyUp("down"))
        //{
        //    InitialTokenAction();
        //}

        if (Input.GetKeyUp("up"))
        {
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
        }
    }

    void useSerial()
    {
        if (serialPort == null)
            return;

        if (string.IsNullOrEmpty(COMPort) || !serialPort.IsOpen) return;

        string output = "";
        try { output = serialPort.ReadLine(); }
        catch (System.Exception) { }

        if (output == "")
        {
            return;
        }

        Debug.LogWarning("read serial port: " + output);
        switch (output)
        {
            case "true":
                Debug.LogWarning("Token on Position " + Position + " is placed!");
                tokenIsPlaced = true;
                if (initialPlacement)
                {
                    Debug.LogWarning("INITIAL PLACEMENT ON POS " + Position);
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        useKeyboard();
        useSerial();
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
        //Debug.LogWarning("[TokenMember.cs] InitialTokenAction() not implemented!");
        //StartCoroutine(RotateMe());

        screen.GetComponent<FadeInOut>().FadeOut(5);
        voiceVisualizer.enabled = true;
        particleSystemRing.Play();
    }

    //TODO: implement
    private void SpeechRequest()
    {
        videoPlayer.clip = videoClips[2];
        videoPlayer.Prepare();
        videoPlayer.waitForFirstFrame = true;
        videoPlayer.isLooping = false;
        videoPlayer.Play();

        Debug.Log("Redewunsch von Member " + Position + ".");
        screen.GetComponent<FadeInOut>().FadeIn(0);
        screen.GetComponent<FadeInOut>().FadeOut(4);
    }

    IEnumerator RotateMe()
    {

        float moveSpeed = 0.1f;
        while (this.transform.rotation.z > 0)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, 0, 0), moveSpeed * Time.time);
            yield return null;
        }
        this.transform.rotation = Quaternion.Euler(0, 0, 0);

        yield return null;
    }
}
