using UnityEngine;


public class NameSpinner : MonoBehaviour
{
    public bool Enabled = false;
    public GameObject ReferenceObject;
    public float RotationSpeed = 0.9f;
    public float AngleMax = 180f;
    public float FadeOutSpeed = .01f;

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


    void Start()
    {
        _alphaCum = 1f;
        _angleCum = 0f;
        _zAxis = Vector3.forward;
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _rotationFinished = false;
        _textMesh = GetComponent<TextMesh>();
        _textColor = _textMesh.color;
    }


    void Update()
    {
        if (!Enabled)
        {
            return;
        }

        if (!_rotationFinished)
        {
            Rotate();
        }
        else
        {
            FadeOut();
        }

        if (_fadeOutFinished)
        {
            Restore();
        }
    }


    private void Rotate()
    {
        var angle = 100 * Time.deltaTime * RotationSpeed;
        if ((_angleCum += angle) <= AngleMax)
        {
            transform.RotateAround(ReferenceObject.transform.position, _zAxis, angle);
        }
        else
        {
            _rotationFinished = true;
        }
    }


    private void Restore()
    {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
        _angleCum = 0f;
        _alphaCum = 1f;
        _textColor.a = 1f;
        _textMesh.color = _textColor;
        Enabled = false;
        _rotationFinished = false;
        _fadeOutFinished = false;
    }


    private void FadeOut()
    {
        var alpha = Time.deltaTime * FadeOutSpeed;
        if ((_alphaCum -= alpha) >= 0f)
        {
            _textColor.a = _alphaCum;
            _textMesh.color = _textColor;
        }
        else
        {
            _fadeOutFinished = true;
        }
    }
}
