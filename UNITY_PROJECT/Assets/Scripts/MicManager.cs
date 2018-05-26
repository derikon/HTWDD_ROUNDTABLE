using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicManager : MonoBehaviour
{
    private List<string> microphones = new List<string>();

    public int RECORDING_DURATION = 100;

    // Use this for initialization
    void Start()
    {
        microphones.AddRange(Microphone.devices);

        foreach (var microphone in microphones)
        {
            Debug.Log("MIC: " + microphone);
        }

        // find all players and objects who is interested into MIC inputs
        var audioReceivers = GameObject.FindGameObjectsWithTag("Player");
        if (audioReceivers.Length == 0)
        {
            Debug.LogWarning("No audio receivers found!");
            return;
        }

        // create temp-queue 
        Queue<GameObject> playerQueue = new Queue<GameObject>();
        foreach (var receiver in audioReceivers)
        {
            playerQueue.Enqueue(receiver);
        }

        // assign every mic to some player
        foreach (var microphone in microphones)
        {
            if (playerQueue.Count == 0)
            {
                Debug.Log("All receivers now have a mic assigned");
                break;
            }
            var receiver = playerQueue.Dequeue();

            var audioComponent = receiver.GetComponent<AudioSource>();
            if (audioComponent == null)
            {
                Debug.LogError("Receiver doesn't have an AudioSource component");
            }

            audioComponent.clip =
                Microphone.Start(microphone, true, RECORDING_DURATION, AudioSettings.outputSampleRate);
        }


        // notify if there are any receivers left after mic-assignment
        if (playerQueue.Count > 0)
        {
            foreach (var receiver in playerQueue.ToArray())
            {
                Debug.LogWarning("Receiver " + receiver.gameObject.name + " has no assigned microphone");
            }
        }
    }
}