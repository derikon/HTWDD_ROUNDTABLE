using UnityEngine;

public class ParticleRingPuls : MonoBehaviour {

	public float RadiusDelta = 0.1f;

	private ParticleSystem _particleSystem;
	private ParticleSystem.ShapeModule _shapeModule;
	private float _defaultRadius;
	private float _minRadius;
	private float _maxRadius;
	private float _radiusDiff;
	private bool _shrink = false;

	
	private void Start() {
		_particleSystem = GetComponent<ParticleSystem>();
		_shapeModule = _particleSystem.shape;
		_defaultRadius = _shapeModule.radius;
		_minRadius = _defaultRadius - RadiusDelta;
		_maxRadius = _defaultRadius + RadiusDelta;
		_radiusDiff = RadiusDelta / 100f;
	}
	
	
	private void Update() {
		if (_shapeModule.radius <= _minRadius) {
			_shrink = false;
		} else if (_shapeModule.radius >= _maxRadius) {
			_shrink = true;
		}

		_shapeModule.radius = (_shrink) ? _shapeModule.radius - _radiusDiff : _shapeModule.radius + _radiusDiff;
	}
}
