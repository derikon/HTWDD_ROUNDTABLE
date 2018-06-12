using UnityEngine;


public class ParticleModifier : MonoBehaviour {

    private ParticleSystem particleSystem;
    private ParticleSystem.NoiseModule noiseModifier;
    private ParticleSystem.ColorOverLifetimeModule colorModifier;
    private ParticleSystem.MinMaxGradient defaultGradient;
    private Gradient actionGradient;

    
	private void Start () {
        this.particleSystem = GetComponent<ParticleSystem>();
        this.colorModifier = this.particleSystem.colorOverLifetime;
        this.noiseModifier = this.particleSystem.noise;
        this.defaultGradient = this.colorModifier.color;
        this.actionGradient = new Gradient();
        this.actionGradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.yellow, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );
	}
	

	private void Update () {
        if (Input.GetKey(KeyCode.P)) {
            this.noiseModifier.positionAmount = .2f;
            this.colorModifier.color = this.actionGradient;

        } else {
            this.noiseModifier.positionAmount = 0f;
            this.colorModifier.color = this.defaultGradient;
        }
	}
}
