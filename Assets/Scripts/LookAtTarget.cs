using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Transform _lookAtTarget;
    [SerializeField] private Transform _alignVector;
    [SerializeField] private float _lerpSpeed = 5f;

    private bool _shouldRotate;
    
    private void FixedUpdate()
    {
        var dot = Vector3.Dot(_alignVector.forward, _lookAtTarget.forward);
        //if (!_shouldRotate) return;

        CalculateRotation(dot < -0.6f ? _lookAtTarget : _alignVector);
    }

    private void CalculateRotation(Transform target)
    {
        var step = _lerpSpeed * Time.deltaTime;
        var direction = target.forward - transform.localPosition;
        var endRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Lerp(transform.rotation, endRotation, step);
    }
}