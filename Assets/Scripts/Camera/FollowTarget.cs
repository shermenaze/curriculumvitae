using System.Collections;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 3f;

    private Vector3 _originalOffset;
    private Vector3 _offset;
    private const float _yOffset = 3.5f;

    private void Awake()
    {
        _offset = transform.position;
        _offset.y -= _yOffset;
    }

    public void SetTarget(Transform target, float waitBeforeSet = 0)
    {
        StartCoroutine(SetTargetAfterDelay(target, waitBeforeSet));
    }

    private IEnumerator SetTargetAfterDelay(Transform target, float delay)
    {
        yield return new WaitForSeconds(delay);
        _target = target;
    }

    public void AddHeight(float newHeight)
    {
        _offset.y += newHeight;
    }

    private void LateUpdate()
    {
        var position = _target.position;
        float newYOffset = Mathf.Clamp(position.y, _yOffset, 10f);
        var newPosition = new Vector3(position.x, newYOffset, position.z);

        var step = _speed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, _offset + newPosition, step);
    }
}