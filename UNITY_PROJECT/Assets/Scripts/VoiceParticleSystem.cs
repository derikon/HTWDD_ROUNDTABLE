using UnityEngine;


public class VoiceParticleSystem : MonoBehaviour {

	public ParticleSystem NoiseSystem;
	public ParticleSystem RingSystem;

	
	private void Start () {
		this.RingSystem.Play();
	}
	
	
	private void Update () {
		if (Input.GetKey(KeyCode.P)) {
			if (!this.NoiseSystem.isPlaying)
				this.NoiseSystem.Play();
		} else {
			if (this.NoiseSystem.isPlaying)
				this.NoiseSystem.Stop();
		}
	}
}
