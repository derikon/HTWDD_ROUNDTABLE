using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicVisualizer : MonoBehaviour
{
    private MicControlC micControl;
    DateTime triggerTime;
    TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0, 200);

    public float loudnessThreshhold;
    public float currentLoudness;
    public ParticleSystem particleSystemNoise;

    public float[] FreqBands = new float[8];
    float average = 0;

    // Use this for initialization
    void Start()
    {
        micControl = GetComponentInChildren<MicControlC>();
        triggerTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: delete MakeFreqBands
        //MakeFreqBands();
        if (DateTime.Now.Subtract(triggerTime) > timeSpan)
        {
            triggerTime = DateTime.Now;
            TriggerParticleSystem();
        }
    }

    private void MakeFreqBands()
    {
        int count = 0;

        var spectrum = micControl.spectrumData;
        int spectrumLength = micControl.spectrumData.Length;

        for (int i = 0; i < 8; i++)
        {
            average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            for (int j = 0; j < sampleCount; j++)
            {
                if (count < spectrumLength)
                {
                    average += micControl.spectrumData[count] * (count + 1);
                }
                count++;
            }

            average /= count;

            FreqBands[i] = average * .1f;
        }
    }

    private void TriggerParticleSystem()
    {
        currentLoudness = Mathf.Round((micControl.loudness < 0.001 ? 0f : micControl.loudness) * 100) / 100;

        if (currentLoudness > loudnessThreshhold)
        {
            if (!particleSystemNoise.isPlaying)
            {
                // Enable playing for at least 1 second
                timeSpan = new TimeSpan(0, 0, 0, 1);
                particleSystemNoise.Play();
                Debug.Log("Noise is playing");
            }
        }
        else
        {
            if (particleSystemNoise.isPlaying)
            {
                // new timespan, so that every 200ms the Method will be checked again
                timeSpan = new TimeSpan(0, 0, 0, 0, 50);
                particleSystemNoise.Stop();
                Debug.Log("Noise stopped now");
            }
        }
    }
}
