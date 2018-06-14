using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.Video;

public class TokenMember : MonoBehaviour
{
    public GameObject[] screens;
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
    private VideoPlayer[] videoPlayers;

    //VoiceParticleSystem voiceParticleSystem;

    [SerializeField]
    private bool initialPlacement = true;

    // supported baudrate is 9600
    private SerialPort serialPort;

    // Use this for initialization
    void Start()
    {
        videoPlayers = GetComponentsInChildren<VideoPlayer>();
        videoPlayers[0].isLooping = true;
        //videoPlayers.clip = videoClips[0];
        videoPlayers[0].Play();
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
            case "1":
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

            case "0":
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

        screens[0].GetComponent<FadeInOut>().FadeOut(1);
        voiceVisualizer.enabled = true;
        particleSystemRing.Play();
        GetComponentInChildren<TextSpinner>().Enabled = true;
    }

    //TODO: implement
    private void SpeechRequest()
    {
        videoPlayers[0].Stop();
        videoPlayers[1].Stop();
        videoPlayers[1].Prepare();
        videoPlayers[1].waitForFirstFrame = true;
        videoPlayers[1].isLooping = false;
        videoPlayers[1].Play();

        Debug.Log("Redewunsch von Member " + Position + ".");
        screens[1].GetComponent<FadeInOut>().FadeIn(0);
        screens[1].GetComponent<FadeInOut>().FadeOut(4);
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
