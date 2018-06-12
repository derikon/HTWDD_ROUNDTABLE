using System;
using UnityEngine;
using System.Collections.Generic;

public class CirclePlacement : MonoBehaviour
{

    public int numObjects = 5;
    public GameObject prefab;
    public float Radius;

    private List<GameObject> players = new List<GameObject>();
    private float oldRadius;
    private DiscussionManager discussionManager;

    COMPortConf comPortConf = new COMPortConf();


    void Start()
    {
        // find discussion manager
        discussionManager = GameObject.FindGameObjectWithTag("DiscussionManager").GetComponent<DiscussionManager>();
        oldRadius = Radius;
        Vector3 center = transform.position;
        for (int i = 0; i < numObjects; i++)
        {
            // TODO: create moderator


            var angle = (360 / numObjects) * i;
            Vector3 pos = GetPosOnCircle(center, Radius, angle);
            Quaternion rot = Quaternion.FromToRotation(Vector3.zero, Vector3.forward);
            players.Add(Instantiate(prefab, pos, rot));

            if (i == 0)
            {
                // Deactivate MemberScript
                var tokenMember = players[i].GetComponentInChildren<TokenMember>();
                tokenMember.enabled = false;

                SpawnModerator();
                var tokenModerator = players[i].GetComponentInChildren<TokenModerator>();
                tokenModerator.Position = i;
                var resultMod = tokenModerator.InitializeComPort(comPortConf.COMPosMod);
            }
            else
            {
                // Deactivate ModeratorScript
                var tokenModerator = players[i].GetComponentInChildren<TokenModerator>();
                tokenModerator.enabled = false;

                // TODO: create static list of possible comports and decide which to
                // use as argument
                var tokenMember = players[i].GetComponentInChildren<TokenMember>();
                var resultMem = tokenMember.InitializeComPort(comPortConf.comPortArray[i - 1]);
                tokenMember.Position = i;
                // Add MicController
                var micController = players[i].GetComponentInChildren<MicControlC>();
                micController.SetDeviceSlot = true;
                micController.InputDevice = i - 1;
                micController.enableSpectrumData = true;
            }

            players[i].transform.Rotate(new Vector3(0, 0, 1), 180 - angle);
            players[i].transform.parent = transform;
            // TODO: will be different if we use TextMeshPro
            var textMesh = players[i].GetComponent<TextMesh>();
            if (textMesh != null)
            {
                textMesh.text = "Player " + i;
            }
        }

        discussionManager.FindAllPlayers();
    }


    void Update()
    {
        // I'm not proud of it but it's hackathon :)
        if (Math.Abs(oldRadius - Radius) < float.Epsilon) return;

        Vector3 center = transform.position;
        for (int i = 0; i < numObjects; i++)
        {
            var angle = (360 / numObjects) * i;
            Vector3 pos = GetPosOnCircle(center, Radius, angle);
            players[i].transform.position = pos;
        }

        oldRadius = Radius;
    }


    void SpawnModerator()
    {
        Debug.Log("Create moderator");
    }

    Vector3 GetPosOnCircle(Vector3 center, float radius, float angle)
    {
        float ang = angle;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
}

public class COMPortConf
{
    //TODO: COM Ports definieren
    public String[] comPortArray = { "22", "", "", "", "" };
    private string cOMPosMod = "";

    public string COMPosMod
    {
        get
        {
            return cOMPosMod;
        }

        set
        {
            cOMPosMod = value;
        }
    }
}