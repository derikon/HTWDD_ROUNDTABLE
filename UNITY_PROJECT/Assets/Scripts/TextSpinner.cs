using UnityEngine;


public class TextSpinner : MonoBehaviour {
    public bool Enabled = false;
    public bool EnableFading = true;
    public GameObject ReferenceObject;
    public float RotationSpeed = 0.9f;
    public float AngleMax = 180f;
    public float FadeOutSpeed = 1f;

    [SerializeField]
    private float _angleCum;
    [SerializeField]
    private float _alphaCum;
    
    private Vector3 _zAxis;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private bool _rotationFinished;
    private bool _fadeOutFinished;
    private TextMesh _textMesh;
    private Color _textColor;


    private void Start() {
        _alphaCum = 1f;
        _angleCum = 0f;
        _zAxis = Vector3.forward;
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _rotationFinished = false;
        _textMesh = GetComponent<TextMesh>();
        _textColor = _textMesh.color;
    }

    
    private void Update() {
        if (!Enabled) {
            return;
        }

        if (!_rotationFinished) {
            Rotate();
        }
        else if (EnableFading) {
            FadeOut();
            Enabled = !_fadeOutFinished;
        }
    }

    
    private void Rotate() {
        var angle = 100 * Time.deltaTime * RotationSpeed;
        if ((_angleCum += angle) <= AngleMax) {
            transform.RotateAround(ReferenceObject.transform.position, _zAxis, angle);
        }
        else {
            _rotationFinished = true;
        }
    }


    private void FadeOut() {
        var alpha = Time.deltaTime * FadeOutSpeed;
        if ((_alphaCum -= alpha) >= 0f) {
            _textColor.a = _alphaCum;
            _textMesh.color = _textColor;
        }
        else {
            _fadeOutFinished = true;
        }
    }
    

    public void Restore() {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
        _angleCum = 0f;
        _alphaCum = 1f;
        _textColor.a = 1f;
        _textMesh.color = _textColor;
        _rotationFinished = false;
        _fadeOutFinished = false;
    }
}
