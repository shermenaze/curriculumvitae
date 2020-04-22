using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 2.5f;

    private Vector3 _offset;

    private void Awake()
    {
        _offset = transform.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _offset + _target.position, _speed * Time.deltaTime);
    }
}
