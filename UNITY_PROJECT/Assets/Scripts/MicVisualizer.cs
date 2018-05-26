using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicVisualizer : MonoBehaviour
{
	private AudioSource audioSource;

	public float[] Samples = new float[512];

	public float [] FreqBands = new float[8];

	// Use this for initialization
	void Start ()
	{
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetSpectrumAudioSource();
		MakeFreqBands();
	}

	private void GetSpectrumAudioSource()
	{
		audioSource.GetSpectrumData(Samples, 0, FFTWindow.Blackman);
	}
	
	private void MakeFreqBands()
	{
		int count = 0;

		for (int i = 0; i < 8; i++)
		{
			float average = 0;
			int sampleCount = (int) Mathf.Pow(2, i) * 2;
			if (i == 7)
			{
				sampleCount += 2;
			}

			for (int j = 0; j < sampleCount; j++)
			{
				average += Samples[count] * (count + 1);
				count++;
			}

			average /= count;

			FreqBands[i] = average * 10;
		}
	}
}
