using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{

	public int Band;
	public float StartScale;
	public float ScaleMultiplier;

	private MicVisualizer micVisualizer;
	
	// Use this for initialization
	void Start ()
	{
		micVisualizer = transform.parent.GetComponentInParent<MicVisualizer>();

		if (micVisualizer == null)
		{
			Debug.LogError("Couldn't find mic visualizer!");
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector3(transform.localScale.x, (micVisualizer.FreqBands[Band] * ScaleMultiplier) + StartScale, transform.localScale.z);
	}
}
