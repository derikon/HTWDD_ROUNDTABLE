using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicVisualizer : MonoBehaviour
{
    private MicControlC micControl;

    public float[] FreqBands = new float[8];

    // Use this for initialization
    void Start()
    {
        micControl = GetComponentInChildren<MicControlC>();
    }

    // Update is called once per frame
    void Update()
    {
        MakeFreqBands();
    }

    private void MakeFreqBands()
    {
        int count = 0;

        var spectrum = micControl.spectrumData;
        int spectrumLength = micControl.spectrumData.Length;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            //if (i == 7)
            //{
            //    sampleCount += 2;
            //}

            //Debug.Log("SampleCount: " + sampleCount);

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
}
