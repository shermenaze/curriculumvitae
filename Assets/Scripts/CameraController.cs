using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 3f;

    private Vector3 _originalOffset;
    private Vector3 _offset;

    private void Awake()
    {
        _offset = transform.position;
        _offset.y -= 3.5f;
        _originalOffset = _offset;
    }

    [ContextMenu("ReturnToOriginalOffset")]
    public void ReturnToOriginalOffset()
    {
        _offset = _originalOffset;
    }
    
    public void AddHeight(float newHeight)
    {
        _offset.y += newHeight;
    }

    private void LateUpdate()
    {
        var position = _target.position;
        float newYOffset = Mathf.Clamp(position.y, 3.5f, 3.5f);
        var newPosition = new Vector3(position.x, newYOffset, position.z) {y = newYOffset};

        var step = _speed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, _offset + newPosition, step);
    }
}