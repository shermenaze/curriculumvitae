using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Camera _camera;

    private NavMeshAgent _agent;
    private Animator _animator;
    private Rigidbody _rigidBody;

    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private float _horizontalInput;
    private float _verticalInput;
    private float _angle;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //GetInput();

        if (Input.GetMouseButton(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                
                
                _agent.SetDestination(hit.point);
            }
        }
        
        Animate();
    }

//    private void FixedUpdate()
//    {
//        var step = Time.deltaTime * _speed;
//        Vector3 moveDirection = _speed * new Vector3(0, 0, _verticalInput);
//        var rotation = new Vector3(0,_horizontalInput * _speed,0);
//
//        transform.Rotate(rotation);
//        _rigidBody.velocity = transform.forward * moveDirection.z;
//    }

    private void Animate()
    {
        //_animator.SetFloat(Horizontal, _angle);
        _animator.SetFloat(Vertical, Mathf.Abs(Mathf.Clamp(_agent.velocity.z, -1, 1)));
    }

    private void GetInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }

    public void UnParent()
    {
        transform.SetParent(null, true);
    }

    public void ParentTo(Transform newParent = null, bool worldPositionStays = true)
    {
        transform.SetParent(newParent, worldPositionStays);
    }
}