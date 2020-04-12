using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform _head = null;
    public Vector3 _lookAtTargetPosition;
    public float _lookAtCoolTime = 0.2f;
    public float _lookAtHeatTime = 0.2f;
    public bool _looking = true;

    private Vector3 _lookAtPosition;
    private Animator _anim;
    private float _lookAtWeight = 0.0f;

    void Start()
    {
        if (!_head)
        {
            Debug.LogError("No head transform - LookAt disabled");
            enabled = false;
            return;
        }

        _anim = GetComponent<Animator>();
        _lookAtTargetPosition = _head.position + transform.forward;
        _lookAtPosition = _lookAtTargetPosition;
    }

    void OnAnimatorIK()
    {
        _lookAtTargetPosition.y = _head.position.y;
        float lookAtTargetWeight = _looking ? 1.0f : 0.0f;

        Vector3 curDir = _lookAtPosition - _head.position;
        Vector3 futDir = _lookAtTargetPosition - _head.position;

        curDir = Vector3.RotateTowards(curDir, futDir,
            6.28f * Time.deltaTime, float.PositiveInfinity);
        _lookAtPosition = _head.position + curDir;

        float blendTime = lookAtTargetWeight > _lookAtWeight ? _lookAtHeatTime : _lookAtCoolTime;
        _lookAtWeight = Mathf.MoveTowards(_lookAtWeight, lookAtTargetWeight, Time.deltaTime / blendTime);
        _anim.SetLookAtWeight(_lookAtWeight, 0.2f, 0.5f, 0.7f, 0.5f);
        _anim.SetLookAtPosition(_lookAtPosition);
    }
}