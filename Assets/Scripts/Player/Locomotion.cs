﻿using UnityEngine;
using UnityEngine.AI;

public class Locomotion : MonoBehaviour
{
    [SerializeField] private float _stoppingVelocity = 0.5f;

    #region Fields

    private Animator _anim;
    private NavMeshAgent _agent;
    private Vector3 _smoothDeltaPosition = Vector3.zero;
    private Vector3 _velocity = Vector3.zero;

    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Vertical = Animator.StringToHash("Vertical");

    #endregion

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponentInChildren<NavMeshAgent>();
    }

    void Start()
    {
        _agent.updatePosition = false;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 worldDeltaPosition = _agent.nextPosition - transform.position;
        
        float deltaX = Vector3.Dot(transform.right, worldDeltaPosition);
        float deltaZ = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector3 deltaPosition = new Vector3(x: deltaX, 0, z: deltaZ);
        
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        _smoothDeltaPosition = Vector3.Lerp(_smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if delta time is safe
        if (Time.deltaTime > 1e-5f)
            _velocity = _smoothDeltaPosition / Time.deltaTime;

        bool shouldMove = _velocity.magnitude > _stoppingVelocity
                          && _agent.remainingDistance > _agent.radius - 0.2f;

        // Update animation parameters
        _anim.SetBool(Walking, shouldMove);
        _anim.SetFloat(Horizontal, _velocity.x);
        _anim.SetFloat(Vertical, _velocity.z);
    }

    void OnAnimatorMove()
    {
        Vector3 position = _anim.rootPosition;
        position.z = _agent.nextPosition.z;
        position.x = _agent.nextPosition.x;
        transform.position = position;
    }
}